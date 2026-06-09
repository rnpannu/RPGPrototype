import random

def display_instructions():
    # read and display the instructions from the wp_instructions text file
    
    filename = 'wp_instructions.txt'
    filemode = 'r'
    infile = open(filename, filemode)
    contents = infile.read()
    infile.close()
    print(contents)
    
def answer_select():
# selects an answer from a editable word bank
    word_bank = ['apple', 'banana', 'watermelon', 'kiwi', 'pineapple', 'mango']
    answer = random.choice(word_bank)
    return answer

def game_loop(answer):
# keeps the game running as long as guesses > 0 and the answer has not been 
# completed
    guesses = 4
    hidden_answer = list('_' * len(answer))
    
    while guesses > 0:
        print('The answer so far is: ' + " ".join(hidden_answer)) 
        if "".join(hidden_answer) == answer:
            break
        letter_guess = input('Guess a letter (' + str(guesses) + ' guesses remaining) :')
        results = guess_check(answer, letter_guess, hidden_answer, guesses) 
        hidden_answer = results[0] 
        guesses = results[1]
        
    return "".join(hidden_answer)

def guess_check(answer, letter_guess, hidden_answer, guesses):
# Checks if the guessed letter is in the answer/has not been guessed 
# Otherwise subtracts a guess.
    if letter_guess in answer and letter_guess not in hidden_answer and len(letter_guess) == 1:
        for letter in range(len(answer)): 
            if letter_guess == answer[letter]:
                hidden_answer[letter] = letter_guess
    elif len(letter_guess) != 1:
        print('Please enter a single letter from a to z')
        guesses = guesses - 1
    else:
        guesses = guesses - 1
    return hidden_answer, guesses
# w
def display_results(answer, hidden_answer_string):
    if hidden_answer_string == answer:
        print('Good job! You found the word ' + answer + '!')
    else:
        print('Not quite, the correct word was ' + answer + '. Better luck next time.')
        
    response = input('Press enter to end the game. ')
       
    
def main():
# the program follows this series of steps: display instructions > 
# choose an answer > establish the hidden answer variable >  run the game loop >
# display the final results
    
    display_instructions()
    
    answer = answer_select()
    
    hidden_answer_string = game_loop(answer)
    
    display_results(answer, hidden_answer_string)
    
main()
