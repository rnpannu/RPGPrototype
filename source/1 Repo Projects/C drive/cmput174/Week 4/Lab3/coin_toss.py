import random
# The flow of the program is as follows: display instructions > run a round > display round results > prompt player to play again > run steps 2-4 again if yes > add and display final results.

# Read instructions file and print the contents
def display_instructions(): 
    filename = 'instructions.txt'
    infile = open(filename, 'r')
    content = infile.read()
    infile.close()
    print(content)

# tosses two coins - independant of player input
def tosser():
    pool = ['H', 'T']    
    first_toss = random.choice(pool)
    second_toss = random.choice(pool)
    print('Player 1 has tossed ' + first_toss + '.')
    print('Player 2 has tossed ' + second_toss + '.')    
    return first_toss, second_toss
# Determines and prints the winner of each try, and keeps track of player tosses and score
def each_round():
    player1_score = 0
    player2_score = 0
    player1_tosses = []
    player2_tosses = []
    for trial in range(4):
        call = input('Heads or Tails? Type H or T > ')
        tosses = tosser() # picks heads or tails for each trial
        
        if call.upper() == tosses[0]:
            print('Player 1 wins.')
            player1_score = player1_score + 1
            
        if call.upper() == tosses[1]:
            print('Player 2 wins.')
            player2_score = player2_score + 1
            
        player1_tosses.append(tosses[0])
        player2_tosses.append(tosses[1])        
            
    return player1_score, player2_score, player1_tosses, player2_tosses 

# Determines and displays the winner of each round, each player's tosses, and 
# the number of times "H, H" repeated in a player's tosses.
def round_results():
    players = each_round()
    player1_tosses = players[2]
    player2_tosses = players[3]
    player1_wins = 0
    player2_wins = 0
    number_of_ties = 0
# players[2] and players[3] require subscription operations so identifiers are best

    print('ROUND STATS: ')  # players[0]/players[1] don't need identifiers
    if players[0] > players[1]: 
        player1_wins = player1_wins + 1
        print('Player 1 wins this round.')
    elif players[1] > players[0]:
        player2_wins = player2_wins + 1
        print('Player 2 wins this round.')
    elif players[0] == players[1]:
        number_of_ties = number_of_ties + 1
        print('This round ends in a tie.')
        
    print("Player 1's points: " + str(players[0]) + '.')
    print("Player 2's points: " + str(players[1]) + '.')    
    print('Player 1 tossed ' + str(player1_tosses))
    print('Player 2 tossed ' + str(player2_tosses))
    

    hh_counter = 0
    hh_counter2 = 0
    for index in range(len(player1_tosses) - 1):
        if player1_tosses[index] + player1_tosses[index + 1] == 'HH':
            hh_counter = hh_counter + 1
        if player2_tosses[index] + player2_tosses[index + 1] == 'HH':
            hh_counter2 = hh_counter2 + 1
    print('HH found in player 1 sequence '+ str(hh_counter) + ' times')
    print('HH found in player 2 sequence '+ str(hh_counter2) + ' times')
    return player1_wins, player2_wins, number_of_ties


# prints the total stats of all rounds to the screen
def summary_stats(number_of_ties, p1_wins, p2_wins):
    print('SUMMARY STATS')
    print('Number of Ties: ' + str(number_of_ties))
    print('Number of Player 1 wins: ' + str(p1_wins))
    print('Number of Player 2 wins: ' + str(p2_wins))
    
    
# The only tasks directly carried out in the main function are the summation and
# printing of total scores and ties, and the while loop that keeps prompting the
# player to play again. The rest of the tasks are done by the previously defined
# functions, that are called at various points in the main function.
def main():

    display_instructions()
    first_round_stats = round_results() # this starts the function ladder 
    number_of_ties = first_round_stats[2]
    p1_wins = first_round_stats[0]
    p2_wins = first_round_stats[1]
    
# a while loop will indefinitely run rounds and iterate scores until y/Y is no 
# longer inputted. The scores are then printed by calling the summary_stats() 
# function. The stats from the first round are then iterated in the while loop
    
    response = input('Would you like to play again? y/N > ')
    while response.upper() == 'Y':     
        total_stats = round_results()
        p1_wins = p1_wins + total_stats[0]
        p2_wins = p2_wins + total_stats[1]
        number_of_ties = number_of_ties + total_stats[2]
        response = input('Would you like to play again? y/N > ')        
    summary_stats(number_of_ties, p1_wins, p2_wins)
      
    
main()