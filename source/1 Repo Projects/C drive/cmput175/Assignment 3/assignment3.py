from AbacoStack import Card, BStack, AbacoStack

def main():
    reply = ''
    play_again = True
    valid = False
    while not valid:
        try: 
            stackamount = int(input('Enter an amount of stacks (2-5): '))
            stackdepth = int(input('Enter the depth of these stacks: '))
            valid = True
        except Exception:
            print('Invalid Entry')    
    numcolors = stackamount
    
    while play_again:
        game_over = False
        abacostack = AbacoStack(stackamount, stackdepth)
        card = Card(numcolors, stackamount)        
        abacostack.show(card)
        
        while not game_over:
            
            move = input('Enter your move(s) [Q to quit and R to reset]: ')
            if move == 'Q':
                game_over = True
            elif move == 'R':
                abacostack.reset()
                card.reset()
            elif len(move) > 14:
                moves = move[:15]
            else:
                moves = move.split() 
            for m in moves:
                try:
                    abacostack.move_Bead(m)
                except Exception as e:
                    print(e)
            
            abacostack.show(card)
            game_over = abacostack.isSolved(card)
            if game_over:
                print('Congratulations! You won in ' + str(abacostack.get_moves()) + ' moves!')
                reply = input('Would you like to play again with another card? Y/N')
                
            play_again = (reply.upper() == 'Y')
            
    
main()   