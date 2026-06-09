def alphabetical_collection(start_letter):
    filename = 'words.txt'
    infile = open(filename, 'r')
    content = infile.read()
    alist = content.splitlines()
    matches = []
    for word in alist:
        if word[0] == start_letter:
            matches.append(word)
    return matches
def main():
    start_letter = input('Enter letter > ')
    words = alphabetical_collection(start_letter)
    print(words)
    
main()