// Input

const text = await Deno.readTextFile("./input.txt");

const monkeysInput = text.split("\n\n").map((e) => e.split("\n"));

// Part 1

type Monkey = {
  items: number[];
  operation: string;
  factor: number;
  divisibleConstraint: number;
  trueConstraint: number;
  falseConstraint: number;
  count: number;
};

function getMonkeyBusinessLevel(rounds: number, divide: boolean) {
  const monkeys: Monkey[] = [];

  for (const monkey of monkeysInput) {
    const operation = monkey[2][23];

    const items = monkey[1]
      .slice(18)
      .split(", ")
      .map((e) => parseInt(e));

    const factor = parseInt(monkey[2].split(operation)[1]);

    const divisibleConstraint = parseInt(monkey[3].split("divisible by")[1]);

    const trueConstraint = parseInt(monkey[4].split("throw to monkey")[1]);
    const falseConstraint = parseInt(monkey[5].split("throw to monkey")[1]);

    monkeys.push({
      items,
      factor,
      divisibleConstraint,
      trueConstraint,
      falseConstraint,
      operation,
      count: 0,
    });
  }

  const divisor = monkeys
    .map((e) => e.divisibleConstraint)
    .reduce((a, b) => a * b);

  for (let i = 0; i < rounds; i++) {
    for (let j = 0; j < monkeys.length; j++) {
      const monkey = monkeys[j];
      for (let worryLevel of monkeys[j].items) {
        monkey.count += 1;

        if (monkey.operation === "+")
          worryLevel += isNaN(monkey.factor) ? worryLevel : monkey.factor;
        else worryLevel *= isNaN(monkey.factor) ? worryLevel : monkey.factor;

        if (divide) worryLevel = Math.floor(worryLevel / 3);
        else worryLevel = worryLevel % divisor;

        if (worryLevel % monkey.divisibleConstraint === 0)
          monkeys[monkey.trueConstraint].items.push(worryLevel);
        else monkeys[monkey.falseConstraint].items.push(worryLevel);
      }

      monkeys[j].items.length = 0;
    }
  }

  return monkeys
    .map((e) => e.count)
    .sort((a, b) => b - a)
    .slice(0, 2)
    .reduce((a, b) => a * b);
}

console.log(getMonkeyBusinessLevel(20, true));

// Part 2

console.log(getMonkeyBusinessLevel(10000, false));
