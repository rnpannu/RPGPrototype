# Remember the Word - Design Document / Version 1 - display, prompt, pause
# Clear screen > Display header > Display instructions 
import random
import time

print('-' * 80)
print('Remember The Word')
print('-' * 80)

filename = 'Remember1Instructions.txt'
filemode = 'r'
opencommand = open(filename, filemode)
instructions = opencommand.read()
opencommand.close()

print(instructions)
# A prompt is displayed asking the player to press enter

input('Press enter key to display the words.')

# Player presses enter > Screen clears
# 4 random words are displayed 
#   - Show word for 0.5 seconds followed by a 0.5 second pause
#   - Each word has a different first letter
print('orange')
time.sleep(0.5)
print('chair')
time.sleep(0.5)
print('sandwich')
time.sleep(0.5)
print('mouse')
time.sleep(0.5)
# They player is given a question promt
#   - Screen displays the first letter of one of the 4 words
#   - The answer is randomly chosen from the 4 randomly chosen words
answer = input('What word started with the letter c? ')
# Check if the player is correct or not
#   - If correct, congratulatory message is displayed
#   - If incorrect, condolence message is displayed
print('Congratulations, you are correct.')
print('The answer was chair.')
print('Sorry you entered ' + answer + '. The answer was chair.')
# The player is given a line prompt
#   - Pressing y or Y restarts the game
#   - Pressing any other key terminates the game. 
finalinput = input('Play again y/N')

