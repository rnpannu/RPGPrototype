def create_grid(filename):
    grid = []
    file = open(filename, 'r')
    rows = int(file.readline())
    print(repr(rows))
    columns = int(file.readline())
    for row in range(rows):
        row = []
        for column in range(columns):
            value = int(file.readline())
            row.append(value)
        grid.append(row)
    return grid
def display_grid(grid):
    # week 9 --> Completed in class acticities
    pass
def 
def main():
    grid = create_grid('data1.txt')
    print(grid)
main()