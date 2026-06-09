# Pre-Poke Framework
# Implements a general game template for games with animation
# You must use this template for all your graphical lab assignments
# and you are only allowed to inlclude additional modules that are part of
# the Python Standard Library; no other modules are allowed

# Adapting framework for new application paint.py

import pygame


# User-defined functions

def main():
   # initialize all pygame modules (some need initialization)
   pygame.init()
   # create a pygame display window
   pygame.display.set_mode((500, 400))
   # set the title of the display window
   pygame.display.set_caption('Paint')   
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
      default_color = 'black'
      brush_size = 3
      self.brush = Brush(default_color, brush_size, self.surface)
      self.painting = False   

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
         elif event.type == pygame.KEYDOWN:
            self.handle_key_down(event)
         elif event.type == pygame.MOUSEBUTTONDOWN:
            self.handle_mouse_down(event)
         elif event.type == pygame.MOUSEMOTION:
            self.handle_mouse_motion(event)
         elif event.type == pygame.MOUSEBUTTONUP:
            self.handle_mouse_up(event)
   
   def handle_mouse_up(self, event):
      self.painting = False
   def handle_mouse_down(self, event):
      self.painting = True
      self.brush.set_center(event.pos)
   def handle_mouse_motion(self, event):
      if self.painting == True:
         self.brush.set_center(event.pos) 
         self.brush.draw()
         self.brush.check_edge()
   def handle_key_down(self, event):
      
      if event.key == pygame.K_r:
         self.brush.set_color('red')   
      elif event.key == pygame.K_g:
         self.brush.set_color('green')
      elif event.key == pygame.K_b:
         self.brush.set_color('blue')
      elif event.key == pygame.K_SPACE:
         self.surface.fill(self.bg_color)
         self.brush.set_color('black')
      

   def draw(self):
      # Draw all game objects.
      # - self is the Game to draw
      
      #self.surface.fill(self.bg_color) # clear the display surface first
      if self.painting:
         self.brush.draw()
         pygame.display.update() # make the updated surface appear on the display

   def update(self):
      # Update the game objects for the next frame.
      # - self is the Game to update

      pass

   def decide_continue(self):
      # Check and remember if the game should continue
      # - self is the Game to check
      
      pass


class Brush:
   # An object in this class represents a Dot that moves 
   
   def __init__(self, dot_color, dot_radius, surface):
      # Initialize a Dot.
      # - self is the Dot to initialize
      # - color is the pygame.Color of the dot
    
      #   coords of the center of the dot
      # - radius is the int pixel radius of the dot
  
      # - surface is the window's pygame.Surface object

      self.color = pygame.Color('black')
      self.radius = dot_radius
      self.center = [250, 200]
      self.surface = surface
   def set_color(self, new_color):
      self.color = pygame.Color(new_color)
   def set_center(self, new_center):
      self.center[0] = new_center[0]
      self.center[1] = new_center[1]
   def check_edge(self):
      size = self.surface.get_size()
      for i in range(0,2):
         if self.center[i] < self.radius:
            self.center[i] = self.radius
         elif self.center[i] > (size[i] - self.radius):
            self.center[i] = (size[i] - self.radius)
            
      
   def move(self):
      # Change the location of the Dot by adding the corresponding 
      # speed values to the x and y coordinate of its center
      # - self is the Dot
      
      for i in range(0,2):
         self.center[i] = (self.center[i] + self.velocity[i])
   
   
   def draw(self):
      # Draw the dot on the surface
      # - self is the Dot
      
      pygame.draw.circle(self.surface, self.color, self.center, self.radius)


main()