// Input

const text = await Deno.readTextFile("./input.txt");

// Part 1

enum Shape {
  Rock = 1,
  Paper = 2,
  Scissors = 3,
}

enum State {
  Win = 6,
  Draw = 3,
  Loss = 0,
}

function charToShape(char: string) {
  if (char === "A" || char === "X") return Shape.Rock;
  if (char === "B" || char === "Y") return Shape.Paper;
  return Shape.Scissors;
}

function getGameState(a: Shape, b: Shape) {
  if (a === Shape.Rock && b === Shape.Paper) return State.Win;
  if (a === Shape.Paper && b === Shape.Scissors) return State.Win;
  if (a === Shape.Scissors && b === Shape.Rock) return State.Win;

  if (a === b) return State.Draw;

  return State.Loss;
}

let points = 0;

for (const line of text.split("\n")) {
  const chars = line.trim().split(" ");

  const firstPlayer = charToShape(chars[0]);
  const secondPlayer = charToShape(chars[1]);

  const gameState = getGameState(firstPlayer, secondPlayer);

  points += secondPlayer + gameState;
}

console.log(points);

// Part 2

function getCorrectShape(a: Shape, strategy: State) {
  if (strategy === State.Win) {
    if (a === Shape.Rock) return Shape.Paper;
    if (a === Shape.Paper) return Shape.Scissors;
    if (a === Shape.Scissors) return Shape.Rock;
  } else if (strategy === State.Loss) {
    if (a === Shape.Rock) return Shape.Scissors;
    if (a === Shape.Paper) return Shape.Rock;
    if (a === Shape.Scissors) return Shape.Paper;
  }
  return a;
}

function charToStrategy(char: string) {
  if (char === "X") return State.Loss;
  if (char === "Y") return State.Draw;
  return State.Win;
}

let points2 = 0;

for (const line of text.split("\n")) {
  const chars = line.trim().split(" ");

  const firstPlayer = charToShape(chars[0]);
  const strategy = charToStrategy(chars[1]);

  const correctShape = getCorrectShape(firstPlayer, strategy);

  points2 += correctShape + strategy;
}

console.log(points2);
