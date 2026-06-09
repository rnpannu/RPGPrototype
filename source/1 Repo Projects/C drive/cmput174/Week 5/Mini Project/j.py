import random

def roll_die(current_player):
# picks an integer between 1 and 6 and returns the value for future use
    input('Player ' + current_player + ' press enter to roll!')
    roll = random.randint(1,6)
    print('Player ' + current_player + ' rolled a ' + str(roll))
    return roll
def display_state(lane, player1_position, player2_position):
# displays the player's current positions and also maintains asterisk condition
# without conditional initial line would be blank so it must go here.
    border = '*' * 36
    if player1_position == player2_position == 0:
        lane[0] = '*'    
    print(border)
    print('update: ' + (' '.join(lane)))
    print(border)
def update_position(player1_position, player2_position, roll, lane, current_player):
# calls the associated player function depending on whose turn it 
# if player position exceeds 7 (8th list position) position stays the same
    if current_player == 'x' and ((player1_position) + roll < 8):
        new_positions = update_player1(player1_position, player2_position, roll, lane)
        return new_positions
    if current_player == 'o' and ((player2_position) + roll < 8):
        new_positions = update_player2(player1_position, player2_position, roll, lane)
        return new_positions
    else:
        print('The roll was too high, player '+ current_player + ' stays in this position')
        return player1_position, player2_position
def update_player1(player1_position, player2_position, roll, lane):
# Updates new position for player 1, deletes old one, and kicks other player 
# if condition is met. 
    p1_old = player1_position
    p1_new = player1_position + roll
    p2_current = player2_position
    if p1_new == p2_current:
        print('x kicked the rival!')
        p2_current = 0 
        lane[p2_current] = 'o'
    lane[p1_old] = '-'
    lane[p1_new] = 'x'
    lane[p2_current] = 'o' # to represent player 2 after the first roll
    return p1_new, p2_current
def update_player2(player1_position, player2_position, roll, lane):
# Updates new position for player 2, deletes old one, and kicks other player
# if condition is met.  
    p2_old = player2_position
    p2_new = player2_position + roll
    p1_current = player1_position
    if p2_new == p1_current:
        print('o kicked the rival!')
        p1_current = 0 
        lane[p1_current] = 'x'
    lane[p2_old] = '-'
    lane[p2_new] = 'o'
    return p1_current, p2_new

def check_game_over(player1_position, player2_position):
# Checks if a player has reached the 8th position and returns the according
# bool value that gets binded to continue_game
    if player1_position == 7:
        print('Player x has won!')
        return False
    elif player2_position == 7:
        print('Player o has won!')
        return False
    else:
        return True
        
def opponent(turn_number):
# Checks which player's turn it is by seeing if the turn number is even or odd
    if (turn_number % 2) == 0:
        
        return 'o'
    else:
       
        return 'x'
def main():
# Establishes initial game values and keeps a loop running as long as a player 
# has not satisfied the game over condition in the check_game_over function.
    continue_game = True
    turn_number = 1
    lane = list('-' * 8)
    player1_position = 0
    player2_position = 0
    while continue_game:
        display_state(lane, player1_position, player2_position)
        continue_game = check_game_over(player1_position, player2_position)
        if not continue_game:
            break
        current_player = opponent(turn_number)
        turn_number = turn_number + 1
        roll = roll_die(current_player)
        new_positions = update_position(player1_position, player2_position, roll, lane, current_player)
        player1_position = new_positions[0] 
        player2_position = new_positions[1] 
    
main()