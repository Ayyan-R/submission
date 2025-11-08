const path = require('path');
const TerserPlugin = require('terser-webpack-plugin');
const JavaScriptObfuscator = require('webpack-obfuscator');

module.exports = (env, argv) => {
  const isProd = argv.mode === 'production';

  return {
    mode: argv.mode,
    entry: './src/index.js',
    output: {
      filename: 'bundle.js',
      path: path.resolve(__dirname, 'dist'),
      clean: true,
    },
    resolve: {
      extensions: ['.ts', '.tsx', '.js', '.jsx'],
    },
    module: {
      rules: [
        {
          test: /\.(ts|tsx)$/,  // fixed regex
          exclude: /node_modules/,
          use: {
            loader: 'babel-loader',
          },
        },
      ],
    },
    optimization: isProd
      ? {
          minimize: true,
          minimizer: [
            new TerserPlugin({
              terserOptions: {
                compress: true,
                mangle: true,
                format: {
                  comments: false,
                },
              },
              extractComments: false,
            }),
          ],
        }
      : {},
    plugins: isProd
      ? [
          new JavaScriptObfuscator(
            {
              rotateStringArray: true,
            },
            ['excluded_bundle_name.js']
          ),
        ]
      : [],
    devtool: isProd ? false : 'source-map',
  };
};
