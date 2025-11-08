try {
    let values: Array<() => number> = [];
    for (var x = 0; x < 4; x++) values.push(() => x);
    console.log("Using var:", values.map(fn => fn()));

    let values1: Array<() => number> = [];
    for (let x = 0; x < 4; x++) values1.push(() => x);
    console.log("Using let:", values1.map(fn => fn()));
} catch (err) {
    console.error("Error in var/let block:", err);
}

try {
    console.log("\n--- const, Object.freeze ---");
    const person: { age: number } = { age: 25 };
    person.age = 30;
    Object.freeze(person);
    person.age = 40; // won’t change because object is frozen

    function freezeObject(obj: { age: number }) {
        Object.freeze(obj);
        obj.age = 50; // won’t change
    }

    const input: { age: number } = { age: 25 };
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
        [Symbol.toPrimitive](hint: string) {
            if (hint === "string") return "Employee details as string";
            if (hint === "number") return this.salary;
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
        name: string;
        role: string;
        group: string;

        constructor(name: string, role: string, group: string) {
            this.name = name;
            this.role = role;
            this.group = group;
        }

        toString(): string {
            return `${this.name} works as ${this.role} in ${this.group}`;
        }
    }

    class Teacher extends Person {
        constructor(name: string, subject: string) {
            super(name, subject, "School");
        }
    }

    class Student extends Person {
        constructor(name: string, grade: string) {
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
    var letters2 = ['a', 'b', 'c'];
    for (var ch of letters2) {
        process.stdout.write(ch + " ");
    }
    console.log("\n");
} catch (err) {
    console.error("Error in for...of loop:", err);
}

try {
    console.log("\n--- ITERATORS .values() ---");
    let iterator = [1, 2, 3].values();
    console.log(iterator.next());
    console.log(iterator.next());
    console.log(iterator.next());
} catch (err) {
    console.error("Error in iterator code:", err);
}

function generateNumbers(n: number = 20): void {
    try {
        if (typeof n !== "number") {
            throw new Error("Invalid input: Only numbers are allowed.");
        }
        const sequence = {
            [Symbol.iterator]() {
                let i = 0;
                return {
                    next(): IteratorResult<number> {
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
    } catch (err: any) {
        console.error("Error in generateNumbers:", err.message);
    }
}

generateNumbers(5);
generateNumbers("a" as any);
console.log();

const ratings: number[] = [5, 4, 5];
let sum = 0;
const asyncAdd = async (a: number, b: number): Promise<number> => a + b;
try {
    ratings.forEach(async (rating) => {
        sum = await asyncAdd(sum, rating);
    });
    console.log("Async sum is:", sum); // Will likely print 0, due to async timing
} catch (err: any) {
    console.log("Error in async block:", err.message);
}

const scores: number[] = [5, 4, 5];
let total = 0;
const add = (a: number, b: number): number => a + b;
try {
    scores.forEach((score) => {
        total = add(total, score);
    });
    console.log("Sync sum is:", total);
} catch (err: any) {
    console.log("Error in sync block:", err.message);
}
