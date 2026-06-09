# Remember The Word Version 1 Code - display, prompt, pause
# Remember Version 4 - clear the screen,
#                    - conditional feedback,
#                    - identify and replace adjacent duplicate line groups with a for statement 
#                    - Sw Quality Requirement
#                         - identify any literal in our program that occur more than once
#                         - bind that literal to an identifier
#                         - replace that literal with the identifier
#                    - random selection of 4 words from a list of words read from a file
#                    - random selection of answer from the 4 words
#                    - player can play the game multiple times 
# Remember Version 4 - identify non-adjacent duplicate line groups
#                    - define a function and place one instance of that line group in the body of the function.
#                    - replace all instances of the line group with a call to the user defined function
import time, os, random

def header():
    
    if os.name == 'nt':
        clear_command = ('cls')
    else:
        clear_command = ('clear')
    os.system(clear_command)
    
    print('*' * 80)
    print('Remember The Word')
    print('*' * 80)
    
    
def instructions(filename):

    file_mode = 'r'
    infile = open(filename,file_mode)
    content = infile.read() # read is a method call
    infile.close()
    print(content)
    
    #   - answer is chosen randomly from the four words that are displayed
    #   - use first letter of answer to prompt player
def randomize_words():
    filename = 'words.txt'
    file_mode = 'r'
    infile = open(filename, file_mode)
    all_words = infile.read()
    #print(repr(all_words))
    all_words_list = all_words.splitlines()
    infile.close()
    #print(all_words_list)
    words = random.sample(all_words_list,4)
    return words

# display four words'-
#   - display word one at a time
#   - 1 sec pause before the word disappears and the next word appears
#   - words are chosen randomly from a list
#   - words start with different letters
def display_words(words):
    pause_time = 1
     
    for word in words:
        print(word)
        time.sleep(pause_time)
        # display header
        header()
    return words

#prompt the player to input a word that they think is the answer
def prompt_for_guess(start_letter):
    guess = input('What word starts with the letter ' + start_letter +'? ')
    return guess

#evaluates the player answer and displays the feedback

def display_results(guess, answer):
    if guess.lower() == answer.lower():
        #   - congratulations message is displayed if player answers correctly
        print('Congratulations, you are corect.')
    else:
        #   - otherwise condolence message
        print('Sorry you entered '+guess+'.')
    print('The answer was '+answer+'.')
    
def prompt_to_continue():
    # prompt player to play again
    #    - program restarts if player chooses to play again
    #    - otherwise program terminates
    reply = input('Play again?y/N').lower()
    continue_game = reply == 'y' # reply == 'y' is evaluated first and the bool assigned to the originally True continue_game identifier
    return continue_game

def main():
    continue_game = True
    filename = 'instructions.txt'
    while continue_game: 
        # clear screen 
        # display header
        header()    
        # display instructions
        instructions(filename)
        
        # prompt player to press enter
        input('Press enter key to display the words.') # ignore the return object from the input function
        
        # clear screen
        # display header
        header()
        
        
        words = randomize_words()
        answer = random.choice(words)
        start_letter = answer[0]       
        display_words(words)
    
        
        
        
        guess = prompt_for_guess(start_letter) # start_letter is the argument for the function call    
        
       
        display_results(guess, answer)
        continue_game = prompt_to_continue()
main()
