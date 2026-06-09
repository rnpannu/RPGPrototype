from Wordle175 import ScrabbleDict
import random

def reformat(container):
    """
    Changes the lists into "sets" for printing
    Inputs: The list to be converted
    Outputs: The reformatted string
    """
    string = '{' 
    for thing in sorted(container): 
            string = string + thing.upper()
            # As long as the values aren't at the end, a comma is added
            if thing != sorted(container)[-1]:
                string = string + ', '
    return string + '}'

def display_results(guess, red, orange, green, results):
    """
    Prints a results string to the screen.
    Inputs: Guessed word, feedback lists, previous results
    Outputs: None
    """
    line = guess.upper() + ' Green = ' + reformat(green) + ' - ' + 'Orange = ' + reformat(orange) + ' - ' + 'Red = ' + reformat(red)
    results.append(line)
    for line in results:
        print(line)
def validate_entry(guess, guessed_words, dictionary):
    """
    Ensures inputted word does not violate any of the guess requirements
    Inputs: Word, list of guessed words, dictionary of all words
    Outputs: None
    """
    if dictionary.check(guess) and guess not in guessed_words:
        return True
    elif guess in guessed_words:
        print(guess.upper() + ' was already entered')    
    elif len(guess) > 5:
        print(guess.upper() + ' is too long')
    elif len(guess) < 5:
        print(guess.upper() + ' is too short')
    elif not dictionary.check(guess):
        print(guess.upper() + ' is not a recognized word')    
        
    return False
        
def guess_duplicates(letter, word):
    """
    Iterates over a word, finding the frequency of the argument letter. Then,
    creates a list with length == frequency with each letter given an index #
    Ex: (e, trees) --> [E0, E1]
    Inputs: Letter we are looking for, word to be searched
    Outputs: List of the indexed letters
    """
    matching = []
    count = find_frequency(letter, word)
    
    for i in range(count):
        matching.append(letter + str(i))
    return matching

def find_frequency(letter, word):
    """
    Finds the frequency of a given letter in the target word
    Inputs: Letter, Target word
    Outputs: Frequency of target word
    """
    count = 0 
 
    for char in word:
        if letter == char:
            count += 1
    return count   
        
def check_game_over(attempts, guess, target_word):
    """
    Prints results if either game over condition is satisfied (out of attempts or word has been found)
    Inputs: Number of attempts, player guess, and target word
    Outputs: None
    """
    if attempts > 7:
        print('Sorry you lose. The Word is ' + target_word.upper())
        return True
    elif guess.lower() == target_word:
        # Attempts must go down one as the rest of the game loop will execute
        print('Found in ' + str(attempts - 1) + ' attempts. Well done. The Word is ' + target_word.upper())
        return True
    return False
def find_spots(undecided, target_word, red, orange, green):
    """
    Finds list positions for letters that are in the word but do not match 
    positions with the target word. 
    Finds the frequency of cases in the letter and assesses current conditions 
    of the existing lists.
    
    Inputs: The undecided letters, target word, position lists
    Outputs: None
    """
    # Asses each case of undecided letters
    for i in range(len(undecided)):
        # creates all possible occurances of a letter for a 5 letter word
        #     - only needed for letters that occur more than once
        plus_one = undecided[i][0] + str(int(undecided[i][1]) + 1)
        plus_two = undecided[i][0] + str(int(undecided[i][1]) + 2)
        minus_one = undecided[i][0] + str(int(undecided[i][1]) - 1)
        minus_two = undecided[i][0] + str(int(undecided[i][1]) - 2)
        indices = [plus_one, plus_two, minus_one, minus_two]
        
        # Division into cases for possible frequencies in a 5 letter word
        if find_frequency(undecided[i][0], target_word) == 1:
            already_in = 0
            for j in indices: 
                # If only one case of letter in the target, and it is already 
                # in a positional list, append to red
                if j in green or j in orange:
                    already_in = True
            if not already_in:
                orange.append(undecided[i])
            else: 
                red.append(undecided[i])
        # Simple expansions to cover increasing amounts of possible letters
        elif find_frequency(undecided[i][0], target_word) == 2:
            already_in = 0
            for j in indices: 
                if j in green or j in orange:
                    already_in += 1
            
                    
            if already_in == 1:
                orange.append(undecided[i])
            elif already_in == 2:
                red.append(undecided[i])
        
        elif find_frequency(undecided[i][0], target_word) == 3:
            already_in = 0
            for j in indices: 
                if j in green:
                    already_in += 1
                if j in orange:
                    already_in += 1
            if already_in in [0, 1, 2]:
                orange.append(undecided[i])
            
            elif already_in == 3:
                red.append(undecided[i])    
def assess_guess(guess, target_word, red, orange, green):
    """
    Compares the inputted guess to the target word and updates the feedback 
    lists accordingly.
    Appends direct matches/not ins to green/red, but holds orange cases for 
    further checks in the find_spots() function
    Inputs: Player guess, targeted word, letter-case lists.
    Outputs: None, but mutables are changed.
    """
    
    cache = {}
    undecided = []
    
    for i in range(len(guess)):
        matching = guess_duplicates(guess[i], guess)
            
        if len(matching) == 1: # Only one of these letters in guess    
            cache[guess[i]] = guess[i]
            # Shared letter in current position between guess and target
            if guess[i] == target_word[i]:
                green.append(cache.get(guess[i]))   
            # letter somewhere in target
            elif guess[i] != target_word[i] and guess[i] in target_word:
                orange.append(cache.get(guess[i]))
            # letter not in target
            elif guess[i] not in target_word:
                red.append(guess[i])              
        elif len(matching) > 1: # Multiple cases of a letter in guess
            # Sets the first letter to letter + '1', may change later
            if cache.get(guess[i]) == None:
                cache[guess[i]] = matching[0]
            
            if guess[i] == target_word[i]:
                # increments cache value by 1 for the next case of letter
                cache[guess[i]] = guess[i] + str(int(cache[guess[i]][1]) + 1)
                green.append(cache[guess[i]])
        
            # 2 cases for a second or third letter, if 
            elif guess[i] != target_word[i] and guess[i] in target_word:
                cache[guess[i]] = guess[i] + str(int(cache[guess[i]][1]) + 1)
                undecided.append(cache[guess[i]])               
            elif guess[i] not in target_word:
                cache[guess[i]] = guess[i] + str(int(cache[guess[i]][1]) + 1)
                red.append(cache[guess[i]])
    # Discerns where to put letters that are in the word but at wrong positions
    find_spots(undecided, target_word, red, orange, green)
                
                
   
                    
def main(): 
    """
    Runs the Wordle175 game. 
    Inputs: None
    Outputs: None 
    """
    # initializing game elements
    attempts = 1
    dictionary = ScrabbleDict(5, 'scrabble5.txt')
    target_word = random.choice(dictionary.getWordList())
    guess = ''
 
    guessed_words = []
    previous_results = []
    # main game loop
    while not check_game_over(attempts, guess, target_word):
        red = []
        green = []
        orange = []
        print('Attempt ' + str(attempts), end = ': ')
        guess = (input('Please enter a five-letter word: ')).lower()
        valid = validate_entry(guess, guessed_words, dictionary)

        if valid:
            
            assess_guess(guess, target_word, red, orange, green) # update lists
            
            display_results(guess, red, orange, green, previous_results)
            guessed_words.append(guess) 
            attempts += 1   
        
    
main()