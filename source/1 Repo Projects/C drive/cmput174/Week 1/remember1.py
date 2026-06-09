# Remember The Word Version 1 Code - Display, Prompt, Pause

import time
# clear screen 
# display header
print('-' * 80)
print('Remember The Word') 
print('-' * 80)

# display instructions

filename = 'instructions.txt'
filemode ='r'
infile = open(filename, filemode) # this identifier holds the data of the file
contents = infile.read() #read is a method call that reads the data
inflike.close()
print(contents)

# prompt player to press enter
input('Press enter key to display the words' ) # we do not care what the player actually inputs

# clear screen
#display 4 words one at a time with 1 second pause in between
# words are chosen randomely from a list. words start with different letters
pause = time.sleep(1)

print('orange')
pause
print('chair')
pause
print('sandwich')
pause
print('mouse')
pause

# prompt plauer to enter a word that starts with the letter of the answer
#   - answer is chosen randomely from the 4 displayed words
guess = input('what word started with the letter c? ')

# evaluate answer and display feedback. The message the player recieves
# depends on whether they answered correctly or not not.
print('Congratulations, you are correct')
print('The answer was chair')

print('Sorry, you entered' + guess + '.')
print('The answer was chair')

# propt player to play again 
#   - If yes, program restarts
#   - If no, program terminates

reply = input('Play again? y/N')
