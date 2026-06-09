class ScrabbleDict:
    def __init__(self, size, filename):
        """
        Initialize attributes for the class
        Inputs: Word length, name of dictionary file
        Outputs: ScrabbleDict Object
        """
        self.size = size
        infile = open(filename, 'r')
        self.scrabble_dict = {}
        self.list_of_lines = infile.readlines()
        
        
        for line in self.list_of_lines:
            # Split the line to access the first element in the line
            cleanline = (line.replace('\n', '')).split(' ')
            # Ignores if the first element length differs from size
            if len(cleanline[0]) == self.size:
                pair = {cleanline[0]: line}
                self.scrabble_dict.update(pair)
                
    def check(self, word):
        """
        Returns whether an inputted word exists in the dictionary
        Inputs: Word
        Outputs: Truth value dependent on the membership condition
        """
        is_in = self.scrabble_dict.get(word)
        return is_in != None
    def getSize(self):
        """
        Returns the length of the dictionary
        Inputs: None
        Outputs: Length integer
        """
        return len(self.scrabble_dict)
    def getWords(self, letter):
        """
        Returns all words in the dictionary with a matching starting letter
        Inputs: Letter
        Outputs: List of corresponding words
        """
        matching = []
        for word in self.scrabble_dict.keys():
            
            if word[0] == letter:
                matching.append(word)
        sorted_list = sorted(matching)
        return sorted_list
    def getWordSize(self):
        """
        Accesses the size attribute
        Inputs: None
        Outputs: Word size integer
        """
        return self.size
    def getMaskedWords(self, template):
        """
        Returns a list of words that have the same revealed letters as template.
        Inputs: Template string
        Outputs: Word list
        """
        
        index_list = []
        valid_words = []
        # Gets character identities for future comparison 
        for char in template:
            if char != '*':
                index_list.append(True)    
            else:
                index_list.append(False)
        # Eliminates words if they don't match the template for a given index
        for word in self.scrabble_dict.keys():
            valid = True
            for i in range(len(word)):
                # If no asterisk and letters don't match ...
                if index_list[i] and word[i] != template[i]:
                    valid = False
            if valid:
                valid_words.append(word)
        return valid_words
                    
    def getConstrainedWords(self, template, letters):
        """
        Returns a list of possible words for the template, given input letters
        for the wild cards.
        
        Inputs: Template string, possible letters
        Outputs: Constrained word list
        """        
        initially_valid = self.getMaskedWords(template)
        index_list = []
        valid_words = []
        temp_list = list(template)
        	
        
        for i, char in enumerate(temp_list):
            # Get indices of unknown characters
            if char == '*':
                index_list.append(i)
        
        # Eliminates words where the number of non-matching characters exceeds 
        # the remaining wildcard amount (len(index_list) - len(letters)).
        for word in initially_valid:
            wrong_letters = 0
            
            for i in index_list:
                # Go through all unknown indices
                if word[i] not in letters:
                   
                    wrong_letters += 1
           
            if not (wrong_letters > (len(index_list) - len(letters))):
                valid_words.append(word)
        return valid_words
            
    def getWordList(self):
        """
        Returns the keys in the dictionary
        Inputs: None
        Outputs: List of dictionary words
        """
        return list(self.scrabble_dict.keys())

if __name__ == "__main__":
    instance = ScrabbleDict(5, 'scrabble5.txt')
    # ---- Check for an existing word ------ 
    print(' ---- Check for an existing word ------ ')
    word = 'outdo'
    print('Checking for: ' + word)
    print(instance.check(word))
    
    # ---- Checking for a nonexistant word --------
    print(' ---- Check for a nonexistant word ------ ')
    word = 'abcde'
    print('Checking for: ' + word)
    print(instance.check(word))
    
    # ---- Checking if a word longer than 5 made it into the dictionary ------
    # For this I put the illegal word in the dictionary temporarily
    print(' ---- Check for an illegal word ------ ')
    word = 'kachow'
    print('Checking for: ' + word)
    print(instance.check(word))    
    # ---- Using the getWords function  ------
    print(' ---- Using the getWords function ------ ')
    letter = 'q'
    stuff = instance.getWords(letter)
    for word in stuff:
        print(word, end =' ')
    # ---- Using the getWords function with an illegal input  ------
    print(' \n' + ' ---- Using the getWords function without a letter input ------ ')
    letter = 1
    stuff = instance.getWords(letter)
    for word in stuff:
        print(word, end =' ')    
    # ---- Using the getWordSize function  ------
    print(' ---- Using the getWordSize function ------ ')
    print('Word Size: ' + str(instance.getWordSize()))