// Input

const text = await Deno.readTextFile("./input.txt");

const input = text.split("\n").slice(1);

// Part 1

type File = {
  name: string;
  size: number;
};
type Directory = {
  name: string;
  subDirectories: Directory[];
  subFiles: File[];
  parentDirectory: Directory | null;
};

const rootDirectory: Directory = {
  name: "/",
  subDirectories: [],
  subFiles: [],
  parentDirectory: null,
};

let currentDirectory: Directory = rootDirectory;

for (let i = 0; i < input.length; i++) {
  const line = input[i];
  if (line.startsWith("$")) {
    const [command, params] = line.slice(2).split(" ");
    if (command === "ls") {
      let j = i + 1;
      while (j !== input.length && !input[j].startsWith("$")) {
        if (input[j].startsWith("dir")) {
          const [_, directoryName] = input[j].split(" ");
          const directory = currentDirectory.subDirectories.find(
            (subDirectory) => subDirectory.name === directoryName
          );

          if (!directory) {
            currentDirectory.subDirectories.push({
              name: directoryName,
              subDirectories: [],
              subFiles: [],
              parentDirectory: currentDirectory,
            });
          }
        } else {
          const [size, fileName] = input[j].split(" ");
          const file = currentDirectory.subFiles.find(
            (file) => file.name === fileName
          );
          if (!file) {
            currentDirectory.subFiles.push({
              name: fileName,
              size: parseInt(size),
            });
          }
        }
        j++;
      }
      i += j - i - 1;
    } else if (command === "cd") {
      if (params === "..") {
        currentDirectory = currentDirectory.parentDirectory!;
      } else {
        const subDir = currentDirectory.subDirectories.find(
          (subdirectory) => subdirectory.name === params
        );
        if (subDir) {
          currentDirectory = subDir;
        } else {
          currentDirectory.subDirectories.push({
            name: params,
            subDirectories: [],
            subFiles: [],
            parentDirectory: currentDirectory,
          });

          currentDirectory =
            currentDirectory.subDirectories[
              currentDirectory.subDirectories.length - 1
            ];
        }
      }
    }
  }
}

function calculateDirectorySum(directory: Directory): number {
  return (
    directory.subFiles.reduce((a, b) => a + b.size, 0) +
    directory.subDirectories
      .map((subDirectory) => calculateDirectorySum(subDirectory))
      .reduce((a, b) => a + b, 0)
  );
}

function findSmallSubdirectories(directory: Directory): Directory[] {
  if (calculateDirectorySum(directory) <= 100000)
    return [
      directory,
      ...directory.subDirectories.flatMap((subDirectory) =>
        findSmallSubdirectories(subDirectory)
      ),
    ];
  return [
    ...directory.subDirectories.flatMap((subDirectory) =>
      findSmallSubdirectories(subDirectory)
    ),
  ];
}

const smallDirectories: Directory[] = findSmallSubdirectories(rootDirectory);

const sumDirectorySize = smallDirectories
  .map((directory) => calculateDirectorySum(directory))
  .reduce((a, b) => a + b, 0);

console.log(sumDirectorySize);

// Part 2

const totalSpaceAvailable = 70000000;

const unusedSpaceNeeded = 30000000;

const currentAvailableSpace =
  totalSpaceAvailable - calculateDirectorySum(rootDirectory);

function findDirectoriesToDelete(directory: Directory): Directory[] {
  if (
    currentAvailableSpace + calculateDirectorySum(directory) >=
    unusedSpaceNeeded
  )
    return [
      directory,
      ...directory.subDirectories.flatMap((subDirectory) =>
        findDirectoriesToDelete(subDirectory)
      ),
    ];

  return [
    ...directory.subDirectories.flatMap((subDirectory) =>
      findDirectoriesToDelete(subDirectory)
    ),
  ];
}

const directoriesToDelete = findDirectoriesToDelete(rootDirectory);

const smallestDirectory = directoriesToDelete.sort(
  (a, b) => calculateDirectorySum(a) - calculateDirectorySum(b)
)[0];

const smallestDirectorySize = calculateDirectorySum(smallestDirectory);

console.log(smallestDirectorySize);
