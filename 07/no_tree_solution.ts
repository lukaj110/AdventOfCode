/*
    Because this challenge does a depth first search, it is possible to solve it
    without constructing a tree structure.

    We solve this by just adding the directories and the size into a stack.
    Once we see a 'cd ..' we delete the last element of the stack and add the size to the directory before it.

    For the second part, we just make a different array of every single directory with its size and calculate
    which one is the smallest.

    If the challenge input had a different traversal path
    we would need to use a different data structure such as a queue or a tree.
*/

// Input

const text = await Deno.readTextFile("./input.txt");

const input = text.split("\n").slice(1);

// Part 1

type Directory = {
  name: string;
  size: number;
};

const dirStack: Directory[] = [{ name: "/", size: 0 }];

const fullDirStack: Directory[] = [];

let sumSmallDirectories = 0;

for (const line of input) {
  if (["$ ls", "dir"].some((e) => line.startsWith(e))) continue;

  if (line.startsWith("$")) {
    const [_, directory] = line.slice(2).split(" ");

    if (directory === "..") {
      const lastDir = dirStack.pop();
      if (lastDir) {
        if (lastDir.size <= 100000) sumSmallDirectories += lastDir.size;
        dirStack[dirStack.length - 1].size += lastDir.size;
        fullDirStack.push({ ...lastDir });
      }
    } else {
      dirStack.push({ name: directory, size: 0 });
    }
  } else {
    const [size, _] = line.split(" ");
    dirStack[dirStack.length - 1].size += parseInt(size);
  }
}

console.log(sumSmallDirectories);

// Part 2

const totalSpaceAvailable = 70000000;

const unusedSpaceNeeded = 30000000;

while (dirStack.length > 0) {
  const lastDir = dirStack.pop();

  if (lastDir) {
    fullDirStack.push({ ...lastDir });

    if (dirStack.length > 0) {
      dirStack[dirStack.length - 1].size += lastDir.size;
    }
  }
}

const totalFreeSpace =
  totalSpaceAvailable - fullDirStack[fullDirStack.length - 1].size;

const directorySizesToDelete = fullDirStack
  .filter((e) => totalFreeSpace + e.size >= unusedSpaceNeeded)
  .sort((a, b) => b.size - a.size);

const smallestDirectorySize =
  directorySizesToDelete[directorySizesToDelete.length - 1].size;

console.log(smallestDirectorySize);
