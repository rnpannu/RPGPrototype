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
    
    
    # main gameplay loop
    close_clicked = False
    while not close_clicked:
        for event in pygame.event.get():
            if event.type == pygame.QUIT: 
                close_clicked = True
    # draw a green circle with radius 100 at position 150, 150 on screen
        circle_color = pygame.Color('green')
        circle_coords = (150, 150)
        circle_radius = 100
        pygame.draw.circle(screen, circle_color, circle_coords, circle_radius)
        rectange_color = pygame.Color('orange')
        rect_left = 0
        rect_top = 50
        rect_width = 200
        rect_height = 50
        rectangle_specs = pygame.Rect(rect_left, rect_top, rect_width, rect_height)
        
        pygame.draw.rect(screen, rectange_color, rectangle_specs)
        
        ball = pygame.image.load('bruh.gif')
        ballrect= ball.get_rect()
        
        # blit draws a source surface on the dest coordinates/rectangle
        # all images are rectangular in shape
        #     # redraws an image onto a destination
        ball_pos = (300, 200) 
        screen.blit(ball, ball_pos)
        
        text_string = 'Hello, World'
        text_color = 'purple'
        text_font = pygame.font.SysFont('Arial', 54, bold=True, italic=True)
        txtimg = text_font.render(text_string, True, text_color)
        text_pos = (250, 200)
        screen.blit(txtimg, text_pos)
        # render text to screen --> requires font either created or default
        # SysFont(name, size) --> pygame.font.Font.render(text, antialias, color)
        
        pygame.display.flip()
        
        
        
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