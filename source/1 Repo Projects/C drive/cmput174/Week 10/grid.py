def create_grid(filename):
    # reads the number of rows and columns and then appends the values to their
    # respective lists
    grid = []
    file = open(filename, 'r')
    rows = int(file.readline())
    columns = int(file.readline())
    for row in range(rows):
        row = []
        for column in range(columns):
            value = int(file.readline())
            row.append(value)
        grid.append(row)
    return grid    
def display_grid(grid):
    # displays each coordinate in the grid with proper borders
    for row_index in range(len(grid)):
        for col_index in range(len(grid[0])):
            cell = grid[row_index][col_index]
            print(' | ' + str(cell) , end = '')   
        print(' |')
def find_neighbors(row_index, col_index, grid):
    # Appends every adjacent coordinate to a list. Limits at the edges of the 
    # grid
    neighbors = []
    max_column_index = len(grid[0]) - 1
    max_row_index = len(grid) - 1
    if col_index > 0 and grid[row_index][col_index - 1] != 0:
        neighbors.append(grid[row_index][col_index -1])
    if col_index < max_column_index and grid[row_index][col_index + 1] != 0:
        neighbors.append(grid[row_index][col_index + 1])
    if row_index > 0 and grid[row_index - 1][col_index] != 0:
        neighbors.append(grid[row_index - 1][col_index])
    if row_index < max_row_index and grid[row_index + 1][col_index] != 0:
        neighbors.append(grid[row_index + 1][col_index])
    if row_index > 0 and col_index > 0 and grid[row_index - 1][col_index - 1] != 0:
        neighbors.append(grid[row_index - 1][col_index - 1])
    if row_index < max_row_index and col_index > 0 and grid[row_index + 1][col_index - 1] != 0:
        neighbors.append(grid[row_index + 1][col_index - 1])
    if row_index > 0 and col_index < max_column_index and grid[row_index - 1][col_index + 1] != 0:
        neighbors.append(grid[row_index - 1][col_index + 1])    
    if row_index < max_row_index and col_index < max_column_index and grid[row_index + 1][col_index + 1] != 0:
        neighbors.append(grid[row_index + 1][col_index + 1])
    return neighbors

def fill_gaps(grid):
    # Observes every coordinate and assigns an value to every missing cell
    # by averaging neighboring cells
    updated_grid = grid
    for row_index in range(len(grid)):
        for col_index in range(len(grid[0])):    
            neighbors = find_neighbors(row_index, col_index, grid)
            mean = (sum(neighbors) // len(neighbors))
            if grid[row_index][col_index] == 0:
                updated_grid[row_index][col_index] = mean
    return updated_grid

def find_max(grid):
    # finds the maximum in each row and then the maximum among the rows
    maximum = 0 
    for row in grid:
        if max(row) > maximum:
            maximum = max(row)
        
    return maximum
def find_average(grid):
    # finds the total price of the entire grid and divides it by the amount of 
    # valid counts
    total = 0
    amount = 0
    for row in grid:
        for column in row:
            if column != 0:
                total = total + column
                amount = amount + 1
            
    return total // amount
    
def main():
    #creates the grid > displays it > replaces unknowns > displays again > stats
    grid = create_grid('data_1.txt')
    
    print('This is our grid: \n ')
    
    display_grid(grid)
    
    new_grid = fill_gaps(grid)
    
    print('\nThis is our newly calculated grid: \n')
    
    display_grid(new_grid)
    
    print('\nSTATS\nAverage housing price in this area is: ' + str(find_average(new_grid)))
    print('Maximum housing price in this area is: ' + str(find_max(new_grid)))
main()    