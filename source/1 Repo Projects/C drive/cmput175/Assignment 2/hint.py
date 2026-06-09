from Wordle175 import ScrabbleDict

def Task4(instance):
    """
    Runs the template hint process. Prints different lists depending on the 
    letters entered (if any)
    Inputs: ScrabbleDict object
    Outputs: None
    """
    asterisk_count = 0
    template = (input('Enter template: ')).lower()
    # Validate template entry
    while len(template) != 5:
        print('Template length must be word length')
        template = (input('Enter template: ')).lower()
    
    # Gets amount of wildcards
    for char in template:
        if char == '*':
            asterisk_count += 1
            
    letters = list(input('Enter any letters: ').lower())
    
    # Validate letters entry
    while len(letters) > asterisk_count:
        print('Amount of letters must be less than amount of unknown indices')
        letters = list(input('Enter any letters: ').lower())
        
    
    # Split input into cases dependent on whether letters are inputted
    if len(letters) == 0:
        masked_words = instance.getMaskedWords(template)
        print(masked_words)
        
    elif len(letters) > 0:
        constrained_words = instance.getConstrainedWords(template, letters)
        print(constrained_words)

def Task5(instance):
    """
    Prints the ScrabbleDict summary statistics for each letter.
    Inputs: ScrabbleDict object
    Outputs: None
    """
    alphabet = 'abcdefghijklmnopqrstuvwxyz'
    stat_cache = {}
    total_cases = 0
    # Creates a cache storing the frequencies for each letter
    for letter in alphabet:
        stat_cache.update({letter: 0})
    # Updates the frequencies for each word in ScrabbleDict and gets the total
    for word in instance.getWordList():
        for letter in word:
            stat_cache[letter] += 1
            total_cases += 1
    # Printing formatting 
    for i in stat_cache.items():
        print(i[0].upper() + ':' + str(i[1]).rjust(5), end = '  ')
        pct = "{:,.2f}%".format((i[1] * 100) / total_cases)
        hist = ''
        for j in range(round((i[1] * 100)/ total_cases)):
            hist += '*'
        print(pct.rjust(6), hist)
        
        
def main():
    """
    Calls the template hints and summary statistic methods
    Inputs: None
    Outputs: None
    """
    
    instance = ScrabbleDict(5, 'scrabble5.txt')
    Task4(instance)
    Task5(instance)

main()