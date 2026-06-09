# Manraj Pannu Lab 2 - Conditional Statements
#      - Categorization under conditons
# The user is prompted to enter the results of the two quizes. 

first_quiz = input('What was the grade on the first quiz? ')
second_quiz = input('What was the grade on the second quiz? ')

first_quiz = float(first_quiz) # Rememeber that input values from the user must
second_quiz = float(second_quiz) # be converted from str to float to be useful.

# A series of conditional statements will sort the results in the different
# categories. The Level 1 and Quiz redo statements will not interfere with
# any other statement, but the two upper categories should go by most 
# restrictive to least restrictive so that the quiz values fall into the proper 
# category first. 

if first_quiz < 50 and second_quiz < 50:
    print('You are assigned to Level 1')
elif first_quiz < 50 and second_quiz >= 50:
    print('Redo Quiz 1')
elif first_quiz >= 50 and second_quiz < 50:
    print('Redo Quiz 2')
elif first_quiz >= 80 and second_quiz >= 80:
    print('You are assigned to Level 3')
elif first_quiz >= 50 and second_quiz >= 50:
    print('You are assigned to Level 2')
else:
    print('Please enter a number from 0 to 100')
    
# Values greater than 100 will fall into Level 3, but the final statement
# is just there to close the conditional statement block. We are assuming 
# values between 0 and 100 in the lab anyway. 
