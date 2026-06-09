# Pre-Poke The Dots Version - Version 2
# learn how to animate basic
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

def main():
    pygame.init()
    size = (500, 400)
    screen = pygame.display.set_mode(size)
    # caption the window
    pygame.display.set_caption('Poke The Dots v0')
    # each iteration of a loop will draw the dot at a static location
    # we move the dot slightly --> redraw the dot and repeat many times a second
    
    # initialize game objects
    circle_color = pygame.Color('green')
    circle_pos = [150, 150]
    circle_velocity = [-15, 1]
    circle_radius = 30
    bg_color = pygame.Color('maroon')
    game_clock = pygame.time.Clock()
    FPS = 30
    # main gameplay loop
    close_clicked = False
    while not close_clicked:
        for event in pygame.event.get():
            if event.type == pygame.QUIT: 
                close_clicked = True
    # draw a circle to screen and iterate its position with color fills
        screen.fill(bg_color)
        pygame.draw.circle(screen, circle_color, circle_pos, circle_radius)
        
        for index in range(0, 2):
            circle_pos[index] = circle_pos[index] + circle_velocity[index]
        pygame.display.flip()
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