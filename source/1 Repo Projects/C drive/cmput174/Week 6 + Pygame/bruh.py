class Rectangle :
    def __init__(self, x, y, width, height):
        self.x = x
        self.y = y
        self.width = width
        self.height = height
        self.area = width * height
    def display(self):
        display_str = 'x = ' + str(self.x) + ' y = ' +str(self.y) + ' width = ' + str(self.width) + ' height = ' + str(self.height)
        return display_str
    def left(self):
        return self.x
    def top(self):
        return self.y
    def right(self):
        return self.x + self.width
    def bottom(self):
        return self.y + self.height
    
    def collide_point(self, point):
# returns true if the point is inside or on the border of the rectangle
        within_x_range = point[0] in range(self.x, self.width + 1)
        within_y_range = point[1] in range(self.y, self.height + 1)
        
        return within_x_range, within_y_range
    
    def collide_rectangle(self, other):
        # other is a rectangle object, returns true if self overlaps with other
        # false otherwise
        self_on_left = self.right() < other.left()
        self_on_right = self.left() > other.right()
        self_above = self.bottom() < other.top()
        self_below = self.top() > other.bottom()
        
        return not(self_on_left or self_on_right or self_above or self_above)
# main program
def main():
    red = Rectangle(25, 50 , 50, 25)
    green = Rectangle(100, 100, 25, 50)
    blue = Rectangle(50, 63, 50, 25)
    print(red.display())
    print(green.display())
    pointa = (50, 63)
    pointb = (113, 125)
    print(red.collide_point(pointa))
    print(green.collide_point(pointb))
    print(red.collide_rectangle(blue))
    print(green.collide_rectangle(blue))
main()