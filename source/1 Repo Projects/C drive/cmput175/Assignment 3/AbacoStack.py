import random

class Card:
    def __init__(self, numcolors, stack_depth):
        """
        Initializes an object of the card class.
        Inputs: Number of unique bead colors, depth of the stacks
        Outputs: None
        """
        self.__coloramount = numcolors
        self.__stackdepth = stack_depth
        self.__no_of_stacks = numcolors
        self.__beads = []
        chars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K']
        
        
        for i in range(0, self.__no_of_stacks):
            self.__chars = chars[:numcolors]
            for j in range(0, self.__stackdepth):
                # selects a set of unique character beads for each stack
                char = random.choice(self.__chars)
                
                self.__chars.remove(char)
                self.__beads.append(char)
    def reset(self):
        """
        Randomizes the card.
        Inputs: None
        Outputs: None
        """        
        random.shuffle(self.__beads)
    def show(self):
        """
        Creates a formal representation of the card, to be shown in the game.
        Inputs: None
        Outputs: None
        """        
        card_str = ''
        counter = 0
        for i in range(0, self.__no_of_stacks):
            
            counter = i
            card_str += '|'
            for j in range(0, self.__stackdepth):
                card_str += self.__beads[counter]
                # increments counter to the ith position in each stack
                counter += self.__stackdepth
                if j != self.__stackdepth - 1:
                    card_str += ' '
            
            card_str += '| \n'
                
        print(card_str)
        
    def stack(self, number):
        """
        Returns a top --> bottom list of a specific stack
        Inputs: Number of stack
        Outputs: Individual stack list
        """        
        if number < 1 or number > self.__no_of_stacks:
            raise Exception('Please input a valid stack')
        start = (number - 1) * self.__no_of_stacks
        # Returns a slice of the stack from the main list
        return self.__beads[start:(start + self.__stackdepth)]
    def __str__(self):
        """
        Informal string representation of the card. |Stack|Stack|...|
        Inputs: None
        Outputs: String representation
        """
        card_str = ''
        counter = 0
        # iterates over the stacks * stackdepth list, with appropriate divides
        for i in range (0, self.__no_of_stacks):
            card_str += '|'
            for j in range(0, self.__stackdepth):
                card_str += self.__beads[counter] 
                counter += 1
            card_str += '|'
        return card_str
    def replace(self, filename, n):
        """
        Replaces the stacks with information from the (n-1) line of a specified
        file
        Inputs: Filename, line index (line number + 1)
        Outputs: None
        """
        infile = open(filename, 'r')
        contents = infile.readlines()
        new_card = contents[n].split()[:self.__no_of_stacks * self.__stackdepth]
        self.__beads = []
        # ------- 2 SOLUTIONS ---------
        # Assignment description is not clear whether lines like: 
        # A B C A B C A B C 
        # are represented as:
        # A A A       A B C 
        # B B B  or   A B C 
        # C C C       A B C 
        # ----------- Main solution solves for left case ----------
        for i in range(self.__no_of_stacks * self.__stackdepth):
            self.__beads.append(new_card[i])
        
        # ------- Alternative solution solves for right case ------
        
        #for i in range(0, self.__no_of_stacks):
                #counter = i
                #self.__beads.append(new_card[counter])
                #counter += self.__stackdepth        
class BStack():
    def __init__(self, cap):
        """
        Initializes an object of the Bstack class.
        Inputs: Stack capacity
        Outputs: None
        """        
        self.__capacity = cap
        self.__items = []
        
    def push(self, item):
        """
        Pushes an object to the top of the stack
        Inputs: Item to be pushed
        Outputs: None
        """           
        if len(self.__items) >= self.__capacity:
            raise Exception('Stack Is Full')
        self.__items.append(item)
    def pop(self):
        """
        Removes and returns an object to the top of the stack
        Inputs: None
        Outputs: Item from top
        """          
        try:
            stuff = self.__items.pop()
            
        except Exception:
            raise Exception('Stack is empty')
        else:
            return stuff              
    def peek(self):   
        """
        Returns an object to the top of the stack
        Inputs: None
        Outputs: Item from top
        """                  
        try:
            stuff = self.items[len(self.items)-1] 
        except:
            raise Exception('Stack is empty')
        else:
            return stuff        
        
    def isEmpty(self):
        """
        Checks if the stack has no items
        Inputs: None
        Outputs: T/F emptiness value
        """                  
        return self.__items == []
    def isFull(self):
        """
        Checks if the stack has hit its item capacity
        Inputs: None
        Outputs: T/F fullness value
        """         
        return len(self.__items) >= self.__capacity 
    def size(self):
        """
        Returns the current size of the stack
        Inputs: None
        Outputs: Stack size
        """          
        return len(self.__items)
    
    def show(self):
        """
        Prints the stack
        Inputs: None
        Outputs: None
        """             
        print(self.__items)
    
    def __str__(self):
        """
        Returns an informal string representation of the stack
        Inputs: None
        Outputs: Stack string
        """             
        stackAsString = ''
        for item in self.__items:
            stackAsString += item + ' '
        return stackAsString
    
    def clear(self):
        """
        Empties the stack
        Inputs: None
        Outputs: None
        """
        if len(self.__items) > 0:
            self.__items.clear()
    def get_items(self):
        """
        Returns the top --> bottom list of a stack
        Inputs: None
        Outputs: Stack list
        """
        newitems = []
        for i in range(len(self.__items)):
            newitems.append(self.__items[len(self.__items) - i - 1])
        
        return newitems
    
class AbacoStack:
    def __init__(self, num, size):
        """
        Initializes an object of the AbacoStack class.
        Inputs: Number of unique colors, size of each stack
        Outputs: None
        """          
        self.__moves = ['1u', '1d', '2u', '2d', '3u', '3d', '0r', '1r', '1l', '2r', '2l', '3r', '3l', '4r', '4l']
        self.__no_of_stacks = num
        self.__stackdepth = size
        self.__movecount = 0
        self.__stacklist = []
        self.__cachedlist = []
        topstr = '. ' + '. ' * self.__no_of_stacks + '.'
        self.__top_row = topstr.split()
        chars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K']
        
        for i in range(0, self.__no_of_stacks):
            stack = BStack(self.__stackdepth)
            
            for bead in range(0, self.__stackdepth):
                stack.push(chars[i])
             
            self.__stacklist.append(stack)
            self.__cachedlist.append(stack)
            
    def move_Bead(self, move):
        """
        Takes a move command and changes the AbacoStack state accordingly.
        Composed of several simple splinter functions.
        Inputs: Move command
        Outputs: None, but changes the stack state
        """
        rownum = int(move[0])
        move_direction = move[1]
        # Checks the validity of the move
        if move not in self.__moves:
            raise Exception('Invalid Move')
        
        valid = False
        # Handles stack pushing cases
        
        if move_direction == 'd' and self.__top_row[rownum] != '.' and not self.__stacklist[rownum - 1].isFull():
            self.handle_down_moves(rownum)
            valid = True       
            
        # Handles stack popping cases
        elif move_direction == 'u' and self.__top_row[rownum] == '.' and not self.__stacklist[rownum - 1].isEmpty():
            self.handle_up_moves(rownum)
            valid = True            
 
        # Handles far left  move case
        
        elif rownum == 0 and self.__top_row[1] == '.':
            self.handle_far_left()
            valid = True       
        # Handles far right left move case
        
        elif rownum == len(self.__top_row) - 1 and self.__top_row[len(self.__top_row) - 2] == '.':
            self.handle_far_right()
            valid = True       
            
        # Handles generic top row left and right cases
        elif rownum > 0 and rownum <= len(self.__stacklist)  and self.__top_row[rownum] != '.':
            if move_direction == 'r' and self.__top_row[rownum + 1] == '.':
                self.handle_right_move(rownum)      
            
            elif move_direction == 'l' and self.__top_row[rownum - 1] == '.': 
                self.handle_left_move(rownum)
            valid = True
        # Checks if a valid move has been made (input was proper)
        if valid:
            self.__movecount += 1
        else:
            raise Exception('Invalid Move')
                
    def handle_down_moves(self, rownum):
        """
        Handles all stack pushing moves
        Inputs: Number of top row position
        Outputs: None
        """
        self.__stacklist[rownum - 1].push(self.__top_row[rownum])       
        self.__top_row[rownum] = '.' 
        
    def handle_up_moves(self, rownum):
        """
        Handles all stack popping moves
        Inputs: Number of top row position
        Outputs: None
        """        
        self.__top_row[rownum] = (self.__stacklist[rownum - 1]).pop()
         
        
    def handle_right_move(self, rownum):
        """
        Handles all stack column right shifting moves
        Inputs: Number of top row position
        Outputs: None
        """        
        self.__top_row[rownum + 1] = self.__top_row[rownum]
        self.__top_row[rownum] = '.'      
        
    def handle_left_move(self, rownum):
        """
        Handles all stack column left shifting moves
        Inputs: Number of top row position
        Outputs: None
        """                
        self.__top_row[rownum - 1] = self.__top_row[rownum]
        self.__top_row[rownum] = '.'
    
    def handle_far_left(self):
        """
        Handles extreme top row left case
        Inputs: Number of top row position
        Outputs: None
        """                
        self.__top_row[1] = self.__top_row[0]
        self.__top_row[0] = '.'
        
    def handle_far_right(self):
        """
        Handles extreme top row left case
        Inputs: Number of top row position
        Outputs: None
        """        
        self.__top_row[len(self.__top_row) - 2] = self.__top_row[len(self.__top_row) - 1]
        self.__top_row[len(self.__top_row) - 1] = '.'        
        
    def isSolved(self, card):
        """
        Checks if the a string representation of the AbacoStack matches the 
        string representation of the Card
        Inputs: Card
        Outputs: Y/N of the match
        """        
        card_str = ''
        for stack in self.__stacklist:
        
            card_str += '|'
            # Gets each stack's item at the specified line
            for j in range(0, self.__stackdepth):
                if stack.isFull():
                    card_str += stack.get_items()[j]
            card_str += '|'
           
                 
        return card_str == str(card)
    def reset(self):
        """
        Rearranges the stack to its initial state
        Inputs: None
        Outputs: None
        """
        self.__moves = 0
        self.__stacklist.clear()
        for stack in self.__cachedlist:
            self.__stacklist.append(stack)
        topstr = '. ' + '. ' * self.__no_of_stacks + '.'
        self.__top_row = topstr.split()
    def show(self, card = None):
        """
        Creates a formal representation of the card, to be shown in the game.
        Inputs: None
        Outputs: None
        """                
        card_str = ''
    
                
        if card:
            for i in range(0, len(self.__top_row)):
                card_str += str(i) + ' '
                
            card_str += '\n' + " ".join(self.__top_row) + ' ' * 7 + 'card' + '\n'
            # Iterates for each line to be printed
            for j in range(0, self.__stackdepth):
                card_str += '| '
                # Gets each stack's item at the specified line
                for stack in self.__stacklist:
                    # Measures the stack's empty spaces (should it not be full)
                    space = self.__stackdepth - stack.size()
                    if space > j:
                        card_str += '. '
                    else:
                        card_str += stack.get_items()[j-space] + ' '

                card_str += '|' + ' ' * 4 + ' |'
                # Prints the card class next to the AbacoStack
                for k in range(1, self.__no_of_stacks + 1):
                    stlist = card.stack(k)
                    if k < self.__no_of_stacks:
                        card_str += stlist[j] + ' '
                    else:
                        card_str += stlist[j]
                card_str += '|\n' 
            card_str += '+' + '-' * 2 *self.__no_of_stacks + '-+' + ' ' * 14 + str(self.__movecount) + ' moves'
    
        else:
            
            for i in range(0, len(self.__top_row)):
                card_str += str(i) + ' '
                
            card_str += '\n' + " ".join(self.__top_row) + ' ' * 7 + 'card' + '\n'
            # Iterates for each line to be printed
            for j in range(0, self.__stackdepth):
                card_str += '| '
                # Gets each stack's item at the specified line
                for stack in self.__stacklist:
                    # Measures the stack's empty spaces (should it not be full)
                    space = self.__stackdepth - stack.size()
                    if space > j:
                        card_str += '. '
                    else:
                        card_str += stack.get_items()[j-space] + ' '

                card_str += '|' + ' ' * 4 + ' |'
        
            
        print(card_str)
    def get_moves(self):
        """
        Returns the move count of the player
        Inputs: None
        Outputs: Number of moves
        """
        return self.__movecount
        

def main():
    card = Card(3, 3)
    card.show()
if __name__ == '__main__': 
    main()
