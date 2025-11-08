// 1️. Basic math + sum of 1–100
function basicMath() {
    try {
        let i = 0;
        i = i + 300 - 200 * 4;
        let sum = 0;
        for (let j = 1; j <= 100; j++) sum += j;
        console.log("Sum of 1–100:", sum);
    } catch (e) {
        console.error("Error in sum loop:", e);
    }
}

// 2️. Closure example
function closureExample() {
    try {
        const vals = [];
        for (let x = 0; x < 4; x++) {
            vals.push(() => x);
        }
        console.log("Closure result:", vals.map(fn => fn()));
    } catch (e) {
        console.error("Error in closure example:", e);
    }
}

// 3️. Object mutation, freeze, seal
function objectExample() {
    try {
        const obj = { par: 3 };
        obj.par = 12;
        console.log("Before freeze:", obj);
        Object.freeze(obj);
        obj.par = 20; // ignored
        Object.seal(obj);
        console.log("After freeze & seal:", obj);
    } catch (e) {
        console.error("Error in object freeze/seal:", e);
    }
}

// 4️. Symbol example
function symbolExample() {
    try {
        const x = Symbol(2);
        const y = Symbol(2);
        const z = x;
        console.log("x == y:", x === y);
        console.log("x == z:", x === z);
    } catch (e) {
        console.error("Error in symbol example:", e);
    }
}

// 5️. Symbol.toPrimitive
function symbolPrimitiveExample() {
    try {
        const js_obj = {
            name: "Ayyan",
            age: 50,
            [Symbol.toPrimitive](hint) {
                if (hint === "string") return "hint over 50";
                if (hint === "number") return 59;
                if (hint === "default") return "hint btw 50-70";
                return null;
            },
        };
        console.log(`${js_obj}`);
        console.log("guess again", js_obj + "");
        console.log("final guess again " + +js_obj);
    } catch (e) {
        console.error("Error in Symbol.toPrimitive example:", e);
    }
}

// 6️. For-of loop
function forOfLoop() {
    try {
        const arr = ["a", "b", "c"];
        for (const i of arr) console.log(i);
    } catch (e) {
        console.error("Error in for-of loop:", e);
    }
}

// 7️. Class + inheritance
function classExample() {
    try {
        class Jedi {
            constructor() {
                this.forceIsDark = false;
            }
            toString() {
                return (this.forceIsDark ? "join" : "fear is the path to") + " the dark side";
            }
        }

        class Sith extends Jedi {
            constructor() {
                super();
                this.forceIsDark = true;
            }
        }

        const yoda = new Jedi();
        const darth = new Sith();
        console.log("yoda:", yoda.toString());
        console.log("Darth Vader:", darth.toString());
    } catch (e) {
        console.error("Error in class example:", e);
    }
}

// 8️. Iterator example
function iteratorExample() {
    try {
        const it = [1, 2, 3, 4, 5][Symbol.iterator]();
        console.log(it.next(), it.next(), it.next(), it.next(), it.next(), it.next());
    } catch (e) {
        console.error("Error in iterator example:", e);
    }
}

// 9️. Generator-like iterator
function generatorExample() {
    try {
        function gen(n) {
            return {
                [Symbol.iterator]() {
                    let i = 0;
                    return {
                        next() {
                            return { done: i > n, value: i++ };
                        },
                    };
                },
            };
        }
        console.log([...gen(20)]);
    } catch (e) {
        console.error("Error in generator example:", e);
    }
}

// 10. Sync sum example
function sumExample() {
    try {
        const ratings = [5, 4, 5];
        let sum = 0;
        const add = (a, b) => a + b;
        ratings.forEach(r => (sum = add(sum, r)));
        console.log("Sum of ratings:", sum);
    } catch (e) {
        console.error("Error in sum example:", e);
    }
}

// 11. Log array elements
function logArrayElements() {
    try {
        const log = (el, i) => console.log(`a[${i}] = ${el}`);
        [2, 3, , 4].forEach(log);
    } catch (e) {
        console.error("Error in logArrayElements:", e);
    }
}

// 12. Entries example
function entriesExample() {
    try {
        const a = [...["a", "b", "f"].entries()];
        console.log("Entries:", a);
    } catch (e) {
        console.error("Error in entries example:", e);
    }
}

// 13. Map + JSON
function mapJsonExample() {
    try {
        const m = new Map([..."abcd"].map(x => [x, x + x]));
        console.log(JSON.stringify([...m]));
        console.log(JSON.stringify([...m.keys()]));
        console.log(JSON.stringify([...m.values()]));
        console.log(JSON.stringify([...m.entries()]));
    } catch (e) {
        console.error("Error in map/json example:", e);
    }
}

// 14. Flatten generator
function flattenExample() {
    try {
        function* flatten(arr) {
            for (const x of arr) {
                if (Array.isArray(x)) yield* flatten(x);
                else yield x;
            }
        }
        console.log([...flatten([1, 2, [3, 4]])]);
    } catch (e) {
        console.error("Error in flatten example:", e);
    }
}

// 15. Destructuring
function destructuringExample() {
    try {
        const a = { x: 1, y: 2 };
        const { x: b, y: z } = a;
        console.log(b, z);
    } catch (e) {
        console.error("Error in destructuring example:", e);
    }
}

// 16. Default values
function defaultValuesExample() {
    try {
        const [a, b = 3, c = 5] = [1, undefined];
        console.log(a, b, c);
    } catch (e) {
        console.error("Error in default values:", e);
    }
}

// 17. Reverse recursion
function reverseExample() {
    try {
        const reverse = ([x, ...y]) => (y.length > 0 ? [...reverse(y), x] : [x]);
        console.log(reverse("anahtrehs".split("")));
        console.log(reverse([1, 2, 3, 4, 5]));
    } catch (e) {
        console.error("Error in reverse example:", e);
    }
}

// 18. Simple square
function squaresExample() {
    try {
        const squares = n => n * n;
        console.log("Square of 20:", squares(20));
    } catch (e) {
        console.error("Error in squares function:", e);
    }
}

// Run all examples
function runAllExamples() {
    console.log("Running all examples...\n");
    basicMath();
    closureExample();
    objectExample();
    symbolExample();
    symbolPrimitiveExample();
    forOfLoop();
    classExample();
    iteratorExample();
    generatorExample();
    sumExample();
    logArrayElements();
    entriesExample();
    mapJsonExample();
    flattenExample();
    destructuringExample();
    defaultValuesExample();
    reverseExample();
    squaresExample();
    console.log("\nAll examples executed.");
}

// Execute everything
runAllExamples();
