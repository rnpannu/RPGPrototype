# Guess the number
import random
guess = input('Enter a number between 1 and 10: ')
number = random.randint(1, 10)
if int(guess) == number:
    print('Congratulations, you are correct.')
    print('Your guess was: ' + guess +'.')
    print('The answer was: ' + str(number) + '.')
else:
    print('Sorry, that was not the correct number.')
    print('Your guess was: ' + guess +'.')
    print('The answer was: ' + str(number) + '.')    
    
