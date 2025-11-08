// 01. Basic Math
export function ex01_math() {
  try {
      let i = 0;
      i = i + 300 - 200 * 4;
      let sum = 0;
      for (let j = 1; j <= 100; j++) sum += j;
      console.log("Sum of 1–100:", sum);
  } catch (e) {
      console.error("Error in ex01_math:", e);
  }
}

// 02. Closure
export function ex02_closure() {
  try {
    const vals: Array<() => number> = [];
for (let x = 0; x < 4; x++) {
    vals.push(() => x);
}
console.log("Closure result:", vals.map(fn => fn()));

  } catch (e) {
      console.error("Error in ex02_closure:", e);
  }
}

// 03. Object Freeze/Seal
export function ex03_object() {
  try {
      const obj = { par: 3 };
      obj.par = 12;
      console.log("Before freeze:", obj);
      Object.freeze(obj);
      obj.par = 20;
      Object.seal(obj);
      console.log("After freeze & seal:", obj);
  } catch (e) {
      console.error("Error in ex03_object:", e);
  }
}

// 04. Symbol
export function ex04_symbol() {
  try {
      const x: symbol = Symbol(2);
      const y: symbol = Symbol(2);
      const z = x;
      console.log("x == y:", x === y);
      console.log("x == z:", x === z);
  } catch (e) {
      console.error("Error in ex04_symbol:", e);
  }
}

// 05. Symbol.toPrimitive
export function ex05_symbolPrimitive() {
  try {
      const js_obj = {
          name: "Ayyan",
          age: 50,
          [Symbol.toPrimitive](hint) {
              if (hint === "string") return "hint over 50";
              if (hint === "number") return 59;
              return "hint btw 50-70";
          },
      };
      console.log(`${js_obj}`);
      console.log("guess again", js_obj + "");
      console.log("final guess again " + +js_obj);
  } catch (e) {
      console.error("Error in ex05_symbolPrimitive:", e);
  }
}

// 06. For-of
export function ex06_forOf() {
  try {
      for (const i of ["a", "b", "c"]) console.log(i);
  } catch (e) {
      console.error("Error in ex06_forOf:", e);
  }
}

// 07. Class + Inheritance
export function ex07_class() {
  try {
      class Jedi {
          forceIsDark: boolean;
         
          constructor() { this.forceIsDark = false; }
          toString() {
              return (this.forceIsDark ? "join" : "fear is the path to") + " the dark side";
          }
      }
      class Sith extends Jedi {
          constructor() { super(); this.forceIsDark = true; }
      }
      console.log("yoda:", new Jedi().toString());
      console.log("darth:", new Sith().toString());
  } catch (e) {
      console.error("Error in ex07_class:", e);
  }
}

// 08. Iterator
export function ex08_iterator() {
  try {
      const it = [1, 2, 3, 4, 5][Symbol.iterator]();
      console.log(it.next(), it.next(), it.next(), it.next(), it.next(), it.next());
  } catch (e) {
      console.error("Error in ex08_iterator:", e);
  }
}

// 09. Generator-like Iterator
export function ex09_generator() {
  try {
      function gen(n: number) {
          return {
              [Symbol.iterator]() {
                  let i = 0;
                  return { next() { return { done: i > n, value: i++ }; } };
              },
          };
      }
      console.log([...gen(10)]);
  } catch (e) {
      console.error("Error in ex09_generator:", e);
  }
}

// 10. Sum Example
export function ex10_sum() {
  try {
      const ratings = [5, 4, 5];
      let sum = 0;
      const add = (a: number, b: number) => a + b;
      ratings.forEach(r => (sum = add(sum, r)));
      console.log("Sum of ratings:", sum);
  } catch (e) {
      console.error("Error in ex10_sum:", e);
  }
}

// 11. Log Array Elements
export function ex11_logArray() {
  try {
      [2, 3, , 4].forEach((el, i) => console.log(`a[${i}] = ${el}`));
  } catch (e) {
      console.error("Error in ex11_logArray:", e);
  }
}

// 12. Entries
export function ex12_entries() {
  try {
      console.log([...["a", "b", "f"].entries()]);
  } catch (e) {
      console.error("Error in ex12_entries:", e);
  }
}

// 13. Map + JSON
export function ex13_mapJson() {
  try {
      const m = new Map([..."abcd"].map(x => [x, x + x]));
      console.log([...m]);
  } catch (e) {
      console.error("Error in ex13_mapJson:", e);
  }
}

// 14. Flatten (recursive generator)
export function ex14_flatten() {
  try {
      function* flatten(arr: any[]): any {
          for (const x of arr)
              Array.isArray(x) ? yield* flatten(x) : yield x;
      }
      console.log([...flatten([1, 2, [3, 4]])]);
  } catch (e) {
      console.error("Error in ex14_flatten:", e);
  }
}

// 15. Destructuring
export function ex15_destructuring() {
  try {
      const a = { x: 1, y: 2 };
      const { x: b, y: z } = a;
      console.log(b, z);
  } catch (e) {
      console.error("Error in ex15_destructuring:", e);
  }
}

// 16. Default Values
export function ex16_defaults() {
  try {
      const [a, b = 3, c = 5] = [1, undefined];
      console.log(a, b, c);
  } catch (e) {
      console.error("Error in ex16_defaults:", e);
  }
}

// 17. Reverse Recursion
export function ex17_reverse() {
  try {
      const reverse = ([x, ...y]: any[]) => (y.length > 0 ? [...reverse(y), x] : [x]);
      console.log(reverse("anahtrehs".split("")));
  } catch (e) {
      console.error("Error in ex17_reverse:", e);
  }
}

// 18. Square Function
export function ex18_square() {
  try {
      const sq = (n: number) => n * n;
      console.log("Square of 20:", sq(20));
  } catch (e) {
      console.error("Error in ex18_square:", e);
  }
}

// Run All
export function runAllExamples() {
  console.log("\n Running all examples...\n");
  ex01_math();
  ex02_closure();
  ex03_object();
  ex04_symbol();
  ex05_symbolPrimitive();
  ex06_forOf();
  ex07_class();
  ex08_iterator();
  ex09_generator();
  ex10_sum();
  ex11_logArray();
  ex12_entries();
  ex13_mapJson();
  ex14_flatten();
  ex15_destructuring();
  ex16_defaults();
  ex17_reverse();
  ex18_square();
  console.log("\n All examples executed successfully.\n");
}
