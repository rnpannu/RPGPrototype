# Remember The Word Version 1 Code - display, prompt, pause
# Remember Version 3 - clear the screen,
#                      conditional feedback,
#                      identify and replace adjacent duplicate line groups with a for statement 
#                      identify any literal in our program that occur more than once and bind that
#                      iteral to an identifier. Random selection of 4 words form a list of words 
#                      read from a file
import time, random, os

def header(symbol, title):
    print(symbol * 80)
    print(title)
    print(symbol * 80)
def instructions():
    filename = 'instructions.txt'
    file_mode = 'r'
    infile = open(filename,file_mode)
    content = infile.read() # read is a method call
    infile.close()
    print(content)
def clear():
    if os.name == 'nt':
        clear_command = ('cls')
    else:
        clear_command = ('clear')
    os.system(clear_command)
# clear screen
clear()
symbols = '-'
game_title = 'Remember The Word'
# display header
header(symbols, game_title)

# display instructions

# prompt player to press enter
input('Press enter key to display the words.') # ignore the return object from the input function

# clear screen
clear()
# display header
header(symbols, game_title)


# display four words'-
#   - display word one at a time
#   - 1 sec pause before the word disappears and the next word appears
#   - words are chosen randomly from a list
#   - words start with different letters

filename = 'words.txt'
infile = open(filename, 'r')
all_words = infile.read()
all_words_list = all_words.splitlines()
words = random.sample(all_words_list, 4)

answer = random.choice(words)
start_letter = answer[0]

pause_time = 1
for word in words:
    print(word)
    time.sleep(pause_time)
    clear()
    time.sleep(pause_time)

header(symbols, game_title)

guess = input(' What word started with the letter ' + start_letter + '? ')

if guess.lower() == answer.lower():
    print('Congratulations, you are correct! ')
    print('The answer was ' + answer + '.')
else:
    print('Sorry, you entered ' + guess + '. ')
    print('The answer was ' + answer + '.')

reply = input('Play again? y/N')

