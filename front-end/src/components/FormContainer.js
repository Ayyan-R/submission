import React from 'react';
import FormComponent from './FormComponent';
import Child1 from './Child1';
import Child2 from './Child2';

const FormContainer = () => {
  return (
    <div style={{ textAlign: 'center', marginTop: '30px' }}>
      <FormComponent />
      <Child1 />
      <Child2 />
    </div>
  );
};

export default FormContainer;
