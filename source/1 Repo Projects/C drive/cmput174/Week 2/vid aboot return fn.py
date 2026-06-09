import os
def read_file(): 
    infile = open('marks.txt', 'r')
    content = infile.read()
    list_of_lines = content.splitlines()
    return list_of_lines
def find_average(lines):
    total = 0
    for string in lines:
        name_,mark = string.split(',')
        total = total + float(mark)
        average = total / (len(lines))
    return average
def find_names(lines, average):
    above_average = []
    for string in lines:
        name,mark = string.split(',')
        if float(mark) > average:
            above_average.append(name)
    return above_average  
def display_result(average, names):
    print('The average mark is: ' + str(average) + '.')
    print('The students that scored above average are: ')
    for name in names:
        print(name)

def main(): 
    #read file
    lines = read_file()
    
    average = find_average(lines)
    
    #find names
    above_average = find_names(lines, average)
    
    #display result
    display_result(average, above_average)
main()