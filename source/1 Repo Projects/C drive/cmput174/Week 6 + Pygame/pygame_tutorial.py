# Pre-Poke Framework
# Implements a general game template for games with animation
# You must use this template for all your graphical lab assignments
# and you are only allowed to inlclude additional modules that are part of
# the Python Standard Library; no other modules are allowed

import pygame


# User-defined functions

def main():
   # initialize all pygame modules (some need initialization)
   pygame.init()
   # create a pygame display window
   pygame.display.set_mode((500, 400))
   # set the title of the display window
   pygame.display.set_caption('A template for graphical games with two moving dots')   
   # get the display surface
   w_surface = pygame.display.get_surface() 
   # create a game object
   game = Game(w_surface)
   # start the main game loop by calling the play method on the game object
   game.play() 
   # quit pygame and clean up the pygame window
   pygame.quit() 


# User-defined classes

class Game:
   # An object in this class represents a complete game.

   def __init__(self, surface):
      # Initialize a Game.
      # - self is the Game to initialize
      # - surface is the display window surface object

      # === objects that are part of every game that we will discuss
      self.surface = surface
      self.bg_color = pygame.Color('black')
      self.FPS = 60
      self.game_Clock = pygame.time.Clock()
      self.close_clicked = False
      self.continue_game = True
      
      # === game specific objects
      self.small_dot = Dot('red', 30, [50, 50], [1, 2], self.surface)
      self.big_dot = Dot('blue', 40, [200, 100], [2, 1], self.surface)
      self.orange_dot = Dot('orange',50,[50,50],[1,1],self.surface)
      self.basket = Basket(250,200,100,50,'purple',[1,0],self.surface)
      self.max_frames = 150
      self.frame_counter = 0

   def play(self):
      # Play the game until the player presses the close box.
      # - self is the Game that should be continued or not.

      while not self.close_clicked:  # until player clicks close box
         # play frame
         self.handle_events()
         self.draw()            
         if self.continue_game:
            self.update()
            self.decide_continue()
         self.game_Clock.tick(self.FPS) # run at most with FPS Frames Per Second 

   def handle_events(self):
      # Handle each user event by changing the game state appropriately.
      # - self is the Game whose events will be handled

      events = pygame.event.get()
      for event in events:
         if event.type == pygame.QUIT:
            self.close_clicked = True

   def draw(self):
      # Draw all game objects.
      # - self is the Game to draw
      
      self.surface.fill(self.bg_color) # clear the display surface first
      self.small_dot.draw()
      self.big_dot.draw()
      self.orange_dot.draw()
      self.basket.draw()
      pygame.display.update() # make the updated surface appear on the display

   def update(self):
      # Update the game objects for the next frame.
      # - self is the Game to update
      
      self.small_dot.move()
      self.big_dot.move()
      self.orange_dot.move()
      self.basket.move()
      self.frame_counter = self.frame_counter + 1

   def decide_continue(self):
      # Check and remember if the game should continue
      # - self is the Game to check
      pass
      #if self.frame_counter > self.max_frames:
      #   self.continue_game = False
class Basket:
   # This class represents a rectangle basket
   def __init__(self,x,y,width,height,color,velocity,surface):
      # -self is the Basket
      # x, y are coordinates of top left corner of the rectangle
      # width and height are the dimensions of the rectangle
      # color is the color of the rectangle
      # velocity is the number of pixels by which the location of the rectangle is changed
      # surface is the surface of the graphical window
      self.rect = pygame.Rect(x,y,width,height)
      self.color = pygame.Color(color)
      self.velocity = velocity
      self.surface = surface
   def draw(self):
      pygame.draw.rect(self.surface,self.color,self.rect)
   def move(self):
      size = self.surface.get_size()
      if self.rect.right > size[0] or self.rect.left < 0:
         self.velocity[0] = -self.velocity[0]
      self.rect.move_ip(self.velocity[0],self.velocity[1])

class Dot:
   # An object in this class represents a Dot that moves 
   
   def __init__(self, dot_color, dot_radius, dot_center, dot_velocity, surface):
      # Initialize a Dot.
      # - self is the Dot to initialize
      # - color is the pygame.Color of the dot
      # - center is a list containing the x and y int
      #   coords of the center of the dot
      # - radius is the int pixel radius of the dot
      # - velocity is a list containing the x and y components
      # - surface is the window's pygame.Surface object

      self.color = pygame.Color(dot_color)
      self.radius = dot_radius
      self.center = dot_center
      self.velocity = dot_velocity
      self.surface = surface
      
   def move(self):
      # Change the location of the Dot by adding the corresponding 
      # speed values to the x and y coordinate of its center
      # - self is the Dot
      size = self.surface.get_size() # size is a tuple (width,height)
      # size[0] -- > width
      # size[1] --> height
      for i in range(0,2):
         self.center[i] = (self.center[i] + self.velocity[i])% size[i]
   
   def draw(self):
      # Draw the dot on the surface
      # - self is the Dot
      
      pygame.draw.circle(self.surface, self.color, self.center, self.radius)


main()