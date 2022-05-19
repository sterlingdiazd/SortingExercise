using System;
using System.Collections;

namespace SortingExercise
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Type the number of strings and then press Enter key: ");
            const int invalidNumberOfStrings = -1;
            int numStrings = invalidNumberOfStrings;

            do
            {
                if (int.TryParse(Console.ReadLine(), out numStrings))
                {
                    string[] stringsToSort = new string[numStrings];

                    for (int i = 0; i < numStrings; i++)
                    {
                        Console.WriteLine("Type the text number {0} you want to sort and then press Enter key: ", i + 1);
                        stringsToSort[i] = Console.ReadLine();
                    }
                    sortingOperations(numStrings, stringsToSort);
                }

                else
                {
                    Console.WriteLine("Number is not in valid format. Try again please.");
                }

            } while (numStrings > invalidNumberOfStrings);
        }

        public static void sortingOperations(int n, string[] array)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Output: ");
            Console.WriteLine(" ");

            for (int i = 0; i < array.Length; i++)
            {
                string currentWord = array[i];
                string outputWord = string.Empty;
                char[] letterOfWord = currentWord.ToCharArray();                
                string[] charFrecuency = getWordFrecuency(letterOfWord);
                int highestFrecuency = 0;
                string[] frecuencySetStringSorted = new string[charFrecuency.Length];
                int occupiedPositions = 0;

                for (int f = 0; f < charFrecuency.Length; f++)
                {
                    string frecuencySetString = charFrecuency[f];
                    if (frecuencySetString == null)
                    {
                        continue;
                    }
                    string[] frecuencySet = frecuencySetString.Split('-');
                    int currentValue = int.Parse(frecuencySet[1]);

                    if (currentValue > highestFrecuency)
                    {
                        highestFrecuency = currentValue;
                        
                        for (int x = occupiedPositions; x > 0; x--)
                        {
                            string currentLastPosition = frecuencySetStringSorted[x - 1];
                            frecuencySetStringSorted[x] = currentLastPosition;
                        }

                        frecuencySetStringSorted[0] = frecuencySetString;
                        occupiedPositions++;
                    }
                    else
                    {
                        frecuencySetStringSorted[occupiedPositions] = frecuencySetString;
                        occupiedPositions++;
                    }
                }

                string[] subArraySortedLexically = new string[frecuencySetStringSorted.Length];
                char[] differentFrecuencies = getDifferentFrecuencies(frecuencySetStringSorted).ToCharArray(); 
                ArrayList arrayByFrecuency = new ArrayList();
                ArrayList finalArray = new ArrayList();

                for (int df = 0; df < differentFrecuencies.Length; df++)
                {
                    int frecuencyValue = int.Parse(differentFrecuencies[df].ToString());
                    arrayByFrecuency = getFrecuencySetsByFrecuencyValue(frecuencySetStringSorted, frecuencyValue);
                    if (arrayByFrecuency.Count > 1)
                    {
                        arrayByFrecuency.Sort();
                    }
                    finalArray.AddRange(arrayByFrecuency);
                }
                for (int fl = 0; fl < finalArray.Count; fl++)
                {
                    string fullFrecuencySet = finalArray[fl].ToString();

                    string[] frecuencySet = fullFrecuencySet.Split('-');
                    int currentValue = int.Parse(frecuencySet[1]);

                    for (int cv = 0; cv < currentValue; cv++)
                    {
                        outputWord += frecuencySet[0];
                    }
                }
                Console.WriteLine(outputWord);
            }
            Console.WriteLine(" ");
            Console.WriteLine("Type the number of strings and then press Enter key: ");
        }

        public static ArrayList getFrecuencySetsByFrecuencyValue(string[] frecuencySetStringSorted, int frecuencyValue)
        {
            ArrayList subArray = new ArrayList();
            for (int sa = 0; sa < frecuencySetStringSorted.Length; sa++)
            {
                string frecuencySetString = frecuencySetStringSorted[sa];
                if (frecuencySetString == null)
                {
                    continue;
                }
                string[] frecuencySet = frecuencySetString.Split('-');
                int currentValue = int.Parse(frecuencySet[1]);
                if (currentValue == frecuencyValue)
                {
                    subArray.Add(frecuencySetString);
                }

            }
            return subArray;
        }
        public static string getDifferentFrecuencies(string[] frecuencySetStringSorted)
        {
            int differentFrecuency = 0;
            int differentFrecuencyIndex = 0;
            string frecuencyValues = string.Empty;
            for (int w = 0; w < frecuencySetStringSorted.Length; w++)
            {
                string frecuencySetString = frecuencySetStringSorted[w];

                if (frecuencySetString == null)
                {
                    continue;
                }
                string[] frecuencySet = frecuencySetString.Split('-');
                int currentValue = int.Parse(frecuencySet[1]);

                if (differentFrecuency != currentValue) 
                {
                    var previousValues = frecuencyValues.ToCharArray();
                    bool equalValue = false;
                    if(previousValues.Length > 0)
                    {
                        for (int i = 0; i < previousValues.Length; i++)
                        {

                            if (int.Parse(previousValues[i].ToString()) == currentValue)
                            {
                                equalValue = true;
                                break;
                            }
                        }
                    } else
                    {
                        equalValue = false;
                    }
                    
                    if (equalValue == false)
                    {
                        differentFrecuency = currentValue;
                        differentFrecuencyIndex++;
                        frecuencyValues += currentValue;
                    }
                }

            }
            return frecuencyValues;
        }

        public static int getFrecuencyOfCharInWord(char[] letterOfWord, char currentChar, int frecuency)
        {
            for (int a = 0; a < letterOfWord.Length; a++)
            {
                char letter = letterOfWord[a];
                if (currentChar.Equals(letter))
                {
                    frecuency++;
                }
            }
            return frecuency;
        }

        public static bool checkForNewCharToCompare(string differentChars, char currentChar)
        {
            bool newCharToCompare = true;
            if (differentChars.Length == 0)
            {
                differentChars += currentChar;
                newCharToCompare = true;
            }
            else
            {
                for (int d = 0; d < differentChars.ToCharArray().Length; d++) 
                {
                    char distinctChar = differentChars.ToCharArray()[d];
                    if (currentChar.Equals(distinctChar))
                    {
                        return false;
                    }
                }
            }
            return newCharToCompare;

        }
        
        public static string[] getWordFrecuency(char[] letterOfWord)
        {
            string[] charFrecuency = new string[letterOfWord.Length];
            string differentChars = String.Empty;
            int frecuency = 0;

            for (int l = 0; l < letterOfWord.Length; l++) 
            {
                char currentChar = letterOfWord[l];

                if (checkForNewCharToCompare(differentChars, currentChar))
                {
                    differentChars += currentChar;
                    frecuency = getFrecuencyOfCharInWord(letterOfWord, currentChar, frecuency);
                }
                else
                {
                    continue;
                }

                var frecuencyOfCurrentChar = frecuency.ToString();
                string letterFrecuency = currentChar.ToString() + '-' + frecuencyOfCurrentChar;
                charFrecuency[l] = letterFrecuency; 
                frecuency = 0;
            }
            return charFrecuency;
        }


    }
}
