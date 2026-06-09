def create_region_list(i, line, lines, region_list):
    
    
    # each line is converted into a list separated at the spaces
    line = line.strip()
    lines[i] = line.split(" ")
    # each line's region is found through indexing and each unique one is 
    # appended into region_list as the start of its respective list
    region = lines[i][-1]
    if [region] not in region_list: # must be encased as region list is [[], []]
        region_list.append([region])    
def add_data(line, lines, region_list):
    index = '[' + line[1] + ', ' + line[0] + ']' # combines date and magnitude
    index_region = line[-1]
    for i, region in enumerate(region_list):
        if index_region == region[0]: # checks if the eq is in this region
            region_list[i].append(index)    

def main():
# opens the source file for reading
    filename = 'earthquake.txt'
    newfile = 'earthquakefmt.txt'
    infile = open(filename, 'r')
    contents = infile.read()
    infile.close()
    lines = contents.splitlines()
    # creates the list of lists that holds each region's information
    quakes = []
    for index, line in enumerate(lines):
        create_region_list(index, line, lines, quakes)
            
    for line in lines:
        add_data(line, lines, quakes)
    # opens the text file for writing
    infile_2 = open(newfile, 'w')
    # writes each region's earthquakes to the text file    
    for quake in quakes:
        quake_string = (str(quake).replace("'", "") + '\n')
        infile_2.write(quake_string)
    infile_2.close()
    
main()