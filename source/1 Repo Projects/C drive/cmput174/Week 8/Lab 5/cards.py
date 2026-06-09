import random 
 
class Card:
    # Creates a Card object with attributes that can be called and displayed
    def __init__(self, rank, suit):
    # defines each Card's numeric rank and suit
        self.rank = rank
        self.suit = suit
      
    def get_rank(self):
    # returns the numeric rank of the Card called
        return self.rank
    def display(self):
    # prints the complete index of the card
        rank_notation = ['Ace', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Jack', 'Queen', 'King']
        print(rank_notation[self.rank-1] +  ' of ' + self.suit)        

    
class Deck:
    # creates a deck of 52 cards divided into 4 suits of 13
    def __init__(self):
        # creates a list of all cards
        suits = ('Clubs', 'Diamonds', 'Hearts', 'Spades')
        self.card_list = []
        for suit in suits:
            for rank in range(1, 14):
                self.card_list.append(Card(rank, suit))
                
    def shuffle(self):
        # randomizes the above list
        random.shuffle(self.card_list)
    
    def deal(self):
        # returns the first card in the shuffled deck and removes that card 
        top_card = self.card_list[0]
        self.card_list.remove(top_card)
        return top_card
class Player:
    # creates each player
    def __init__(self):
        # creates each Player's initally empty hand list
        self.hand = []
    def add(self, card):
        # appends a card into each player's hand list
        self.hand.append(card)
    def ace_cards(self):
        # counts the number of ace cards in each player's hand does so by
        # getting the rank of each card object and comparing it to an Ace
        ace_counter = 0
        for card in self.hand:
            if card.get_rank() == 1:
                ace_counter += 1
        return ace_counter
    def display(self):
        # calls on the display method of each card in each Player's hand
        for card in self.hand:
            card.display()
            
def main():
    # runs the game loop as long as there is no winner
    # Creates and shuffles a deck, creates two players and displays their hands
    winner = False
    while not winner:
        deck = Deck()
        deck.shuffle()
        Player_1 = Player()
        Player_2 = Player()
       
        
        for card in range(0, 5):

            Player_1.add(deck.deal())
            Player_2.add(deck.deal())
            player1_aces = Player_1.ace_cards()
            player2_aces = Player_2.ace_cards()
            
        print('\nThis is the hand of player 1: ')
        Player_1.display()
        print('\nThis is the hand of player 2: ')
        Player_2.display()
        # each player's ace cards are counted then compared
        print("\nNumber of ace cards in each player's hand: ")
        print('Player 1 has '+ str(player1_aces) + ' aces.')
        print('Player 2 has '+ str(player2_aces) + ' aces.')
        print('\nResult:')
    
        if player1_aces > player2_aces:
            print('Player 1 is the winner')
           
            winner = True
        elif player1_aces < player2_aces:
            print('Player 2 is the winner')
           
            winner = True
        else:
            print('No winner, shuffle again')
    # loop only runs again if winner stays false
      
main()
            
            