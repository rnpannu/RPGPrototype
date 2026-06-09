# CMPUT 175 Assignment 2 - Raj Pannu

def main():
# ----- Read and interpret source file -------
    filename = 'word5Dict.txt'
    infile = open(filename, 'r')
    contents = infile.read()
    words = contents.split('#')
    # ----- Write single word lines to new file ----
    newfile = 'scrabble5.txt'
    in_newfile = open(newfile, 'w')
    
    for word in words:
        print(word, file = in_newfile)
    in_newfile.close()
main()