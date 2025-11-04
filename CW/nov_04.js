try {
    let values = [];
    for (var x = 0; x < 4; x++) values.push(() => x);
    console.log("Using var:", values.map(fn => fn()));

    let values1 = [];
    for (let x = 0; x < 4; x++) values1.push(() => x);
    console.log("Using let:", values1.map(fn => fn()));
} catch (err) {
    console.error("Error in var/let block:", err);
}

try {
    console.log("\n--- const, Object.freeze ---");
    const person = { age: 25 };
    person.age = 30;
    Object.freeze(person);
    person.age = 40;
    function freezeObject(obj) {
        Object.freeze(obj);
        obj.age = 50;
    }
    const input = { age: 25 };
    freezeObject(input);
    console.log("After freeze:", input.age);
} catch (err) {
    console.error("Error in freeze block:", err);
}

try {
    console.log("\n--- Shorthand Object Property ---");
    let x = 3, y = 5;
    let point = { x, y };
    console.log(point);
} catch (err) {
    console.error("Error in shorthand block:", err);
}

try {
    console.log("\n--- Symbols ---");
    let s1 = Symbol("test");
    let s2 = Symbol("test");
    console.log("Symbols equal?", s1 === s2);
    const employee = {
        name: "John",
        salary: 600,
        [Symbol.toPrimitive](hint) {
            if (hint == "string") return "Employee details as string";
            if (hint == "number") return this.salary;
            return JSON.stringify(this);
        }
    };
    console.log(employee);
    console.log("String conversion:", String(employee));
} catch (err) {
    console.error("Error in symbol block:", err);
}

try {
    console.log("\n--- Classes & Inheritance ---");
    class Person {
        constructor(name, role, group) {
            this.name = name;
            this.role = role;
            this.group = group;
        }
        toString() {
            return `${this.name} works as ${this.role} in ${this.group}`;
        }
    }
    class Teacher extends Person {
        constructor(name, subject) {
            super(name, subject, "School");
        }
    }
    class Student extends Person {
        constructor(name, grade) {
            super(name, grade, "School");
        }
    }
    console.log(new Teacher("Ravi", "Maths").toString());
    console.log(new Student("Meena", "10th Grade").toString());
} catch (err) {
    console.error("Error in class block:", err);
}

try {
    console.log("\n--- ITERATORS WITH var AND hasOwnProperty ---");
    var letters = ['a', 'b', 'c'];
    for (var i in letters) {
        if (letters.hasOwnProperty(i)) {
            process.stdout.write(i + " ");
        }
    }
    console.log("\n");
} catch (err) {
    console.error("Error in for...in loop:", err);
}

try {
    console.log("\n--- ITERATORS WITH of ---");
    var letters = ['a', 'b', 'c'];
    for (var i of letters) {
        process.stdout.write(i + " ");
    }
    console.log("\n");
} catch (err) {
    console.error("Error in for...of loop:", err);
}

try {
    console.log("\n--- ITERATORS .values()---");
    let iterator = [1, 2, 3].values();
    console.log(iterator.next());
    console.log(iterator.next());
    console.log(iterator.next());
} catch (err) {
    console.error("Error in iterator code:", err);
}

function generateNumbers(n = 20) {
    try {
        if (typeof n !== "number") {
            throw new Error("Invalid input: Only numbers are allowed.");
        }
        const sequence = {
            [Symbol.iterator]() {
                let i = 0;
                return {
                    next() {
                        return {
                            done: i > n,
                            value: i++
                        };
                    }
                };
            }
        };
        process.stdout.write("Numbers: ");
        for (let num of sequence) {
            process.stdout.write(num + " ");
        }
        console.log();
    } catch (err) {
        console.error("Error in generateNumbers:", err.message);
    }
}

generateNumbers(5);
generateNumbers("a");
console.log();

const ratings = [5, 4, 5];
let sum = 0;
const asyncAdd = async (a, b) => a + b;
try {
    ratings.forEach(async (rating) => {
        sum = await asyncAdd(sum, rating);
    });
    console.log("Async sum is:", sum);
} catch (err) {
    console.log("Error in async block:", err.message);
}

const scores = [5, 4, 5];
let total = 0;
const add = (a, b) => a + b;
try {
    scores.forEach((score) => {
        total = add(total, score);
    });
    console.log("Sync sum is:", total);
} catch (err) {
    console.log("Error in sync block:", err.message);
}
