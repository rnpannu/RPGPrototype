# Pre-Poke The Dots Version - Version 4
# working on splitting up learned concepts specific to poke the dots or games in general

# learn how to animate basic geometric shapes
# we are learning how to do the following things:
#
# learn how to open a graphical window - DONE!
# learn how to keep the graphical window open until the Close button is pressed - DONE!
# set title window - DONE!
# draw basic geometric shapes
#   draw a circle - DONE!
#   draw a rectangle - DONE!
# change the color of geometric shapes - DONE!
# respond to user input (close window) - DONE!
#
# Some of the source code contained in this program is not original. It was borrowed from
# a tutorial found on pygame's website. Specifically, we used portions of this tutorial
# to respond to QUIT events and close the PyGame grapical window, to create a
# game window, and to understand how to use the flip() function to render graphics.
# https://www.pygame.org/docs/tut/PygameIntro.html

import pygame

class Dot:
    def __init__(self, dot_color, dot_radius, dot_center, dot_velocity, screen):
        self.color = dot_color
        self.velocity = dot_velocity
        self.radius = dot_radius
        self.center = dot_center
        self.screen = screen
    def move(self):
        for index in range(0, 2):
            self.center[index] = self.center[index] + self.velocity[index]
    def draw(self):
        pygame.draw.circle(self.screen, self.color, self.center, self.radius)
        
        
def main():
    pygame.init()
    size = (500, 400)
    screen = pygame.display.set_mode(size)
    # caption the window
    pygame.display.set_caption('Poke The Dots v0')
    # each iteration of a loop will draw the dot at a static location
    # we move the dot slightly --> redraw the dot and repeat many times a second
    
    
    # initialize game objects
    
    frame_counter = 0
    max_frames = 100    
    
    big_dot_color = pygame.Color('yellow')
    big_dot_pos = [150, 150]
    big_dot_velocity = [1, 1]
    big_dot_radius = 50
    
    big_dot = Dot(big_dot_color, big_dot_radius, big_dot_pos, big_dot_velocity, screen)
    
    small_dot_color = pygame.Color('red')
    small_dot_pos = [300, 150]
    small_dot_velocity = [2, 2]
    small_dot_radius = 20
    
    small_dot = Dot(small_dot_color, small_dot_radius, small_dot_pos, small_dot_velocity, screen)
    # game objects & vars general to games
    continue_game = True
    
    bg_color = pygame.Color('black')
    game_clock = pygame.time.Clock()
    FPS = 30
    # main gameplay loop
    close_clicked = False
    while not close_clicked and continue_game:
        for event in pygame.event.get():
            if event.type == pygame.QUIT: 
                close_clicked = True
    # draw a circle to screen and iterate its position with color fills
        screen.fill(bg_color)
        
        small_dot.draw()
        big_dot.draw()
        small_dot.move()
        big_dot.move()        
        
        
        pygame.display.flip()
    
        # check over game over conditions are met and iterate over game state
        if continue_game:
            
            frame_counter = frame_counter + 1
        
            if frame_counter > max_frames:
                continue_game = False
        game_clock.tick(FPS)
        
        
        
main()













#def main():
    ## initialize pygame -- this is required for rendering fonts
    #pygame.init()
    
    ## create the window and set its size to 500 width and 400 height
    #size = (500, 400)
    #screen = pygame.display.set_mode(size)
    
    ## set the title of the window
    #pygame.display.set_caption("Poke The Dots Prepration v0")
    
    ## enter our main gameplay loop, repeating until the user clicks
    ## the window's 'close' button
    #close_clicked = False
    #while not close_clicked:
        #for event in pygame.event.get():
            #if event.type == pygame.QUIT: 
                #close_clicked = True
        
        ## draw a green circle with radius 100 at position 150, 150 on screen 
        #circle_color = pygame.Color('green')
        #circle_pos = (150, 150)
        #circle_radius = 100
        #pygame.draw.circle(screen, circle_color, circle_pos, circle_radius)
        
        ## draw a blue rectangle with height 50 and with 200 at position 0, 0 on screen
        #rect_color = pygame.Color('blue')
        #rect_left = 0
        #rect_top = 0
        #rect_width = 200
        #rect_height = 50
        #rect_params = pygame.Rect(rect_left, rect_top, rect_width, rect_height)
        #pygame.draw.rect(screen, rect_color, rect_params)
        
        ## render all drawn objects to the screen
        #pygame.display.flip()
        
#main()