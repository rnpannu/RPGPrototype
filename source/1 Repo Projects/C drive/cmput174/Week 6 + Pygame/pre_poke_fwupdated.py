# Pre-Poke v3 
# You must use this template for all your graphical lab assignments
# and you are only allowed to inlclude additional modules that are part of
# the Python Standard Library; no other modules are allowed
# TO DO LIST:
# Make scoreboard of 72 pt font that updates every second
#    - w.surface.blit()
#    - pygame.clock.get_ticks()
# Ensure the game over condition is met when the dots collide
#    - Compute the distance between the two dots using the Pythagoerean theorem
#    - If the two radii are greater than this distance, they have collided
# Display a GAME OVER message in the bottom left corner of the window
#    - Find the coordiantes for the top left corner using (0 , size[1] - width)
#    - message.Rect.get_height() or message.Rect.get_size()[1]
# Handle the player mouse click
#    - Have the handle_events() method recognize the MOUSEBUTTONUP event
#    - randomize the dot position using the randomize function


import pygame, random, math, time


# User-defined functions

def main():
   # initialize all pygame modules (some need initialization)
   pygame.init()
   # create a pygame display window
   pygame.display.set_mode((500, 400))
   # set the title of the display window
   pygame.display.set_caption('Poke The Dots')   
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
      self.score  = 0
      self.small_dot = Dot('red', 30, [50, 50], [1, 2], self.surface)
      self.big_dot = Dot('blue', 40, [200, 100], [2, 1], self.surface)      
      self.dots = self.create_dots()
   def create_dots(self):
      # === game specific objects ====
      
      self.small_dot.randomize()
      self.big_dot.randomize()         
      while self.small_dot.collide(self.big_dot):
         self.small_dot.randomize()
         self.big_dot.randomize()            

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
         print(event)
         if event.type == pygame.QUIT:
            self.close_clicked = True
         if event.type == pygame.MOUSEBUTTONUP and self.continue_game:
            self.handle_mouse_up()
   def handle_mouse_up(self):
      self.big_dot.randomize()
      self.small_dot.randomize()
   def draw(self):
# Draw all game objects.
# - self is the Game to draw
      self.surface.fill(self.bg_color) # clear the display surface first
      self.small_dot.draw()
      self.big_dot.draw()
      self.draw_score()
      if self.continue_game == False:
         self.draw_game_over()
      pygame.display.update() # make the updated surface appear on the display
      
   def draw_score(self):
      # draw the score at the top left corner of the window
      score_string = ('Score: ' + str(self.score))
      fg_color = pygame.Color('white')
# create a font object
      font = pygame.font.SysFont('Comic Sans', 80)
      text_box = font.render(score_string, True, fg_color, self.bg_color)
      y = 0
      x = self.surface.get_width() - text_box.get_width()
      location = (x , y)      
      self.surface.blit(text_box, location)
   def draw_game_over(self):
      message = 'GAME OVER'
      font = pygame.font.SysFont('', 100)
      fg_color = self.small_dot.get_color()
      bg_color = self.big_dot.get_color()
      text_box = font.render(message, True, fg_color, bg_color)
      location = (0, self.surface.get_height() - text_box.get_height())
      self.surface.blit(text_box, location)
# must blit the source surface onto the target surface at the specified location
   def update(self):
# Update the game objects for the next frame.
# - self is the Game to update
      
      self.small_dot.move()
      self.big_dot.move()
      self.score = (pygame.time.get_ticks()) // 1000

   def decide_continue(self):
      # Check and remember if the game should continue
      # - self is the Game to check
      if self.small_dot.collide(self.big_dot):
         self.continue_game = False
         
      #if self.frame_counter > self.max_frames:
         #self.continue_game = False


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
   
   def randomize(self):
      size = self.surface.get_size()
      for i in range(2):
         self.center[i] = random.randint(self.radius, (size[i] - self.radius))
   def collide(self, other):
# returns true if self collides with other, otherwise false
# self is the the dot, other is also a dot
      distance_x = self.center[0] - other.center[0]
      distance_y = self.center[1] - other.center[1]
      hypotenuse = math.sqrt((distance_x**2) + (distance_y**2))
      
      return hypotenuse <= (self.radius + other.radius)
      
      
   def move(self):
      # Change the location of the Dot by adding the corresponding 
      # speed values to the x and y coordinate of its center
      # - self is the Dot
      size = self.surface.get_size()
      for i in range(2):
         self.center[i] = (self.center[i] + self.velocity[i])
         if self.center[i] <= self.radius or self.center[i] >= (size[i] - self.radius):
            self.velocity[i] = -self.velocity[i]
            
   
   def draw(self):
      # Draw the dot on the surface
      # - self is the Dot
      
      pygame.draw.circle(self.surface, self.color, self.center, self.radius, 5)
   def get_color(self):
      return self.color


main()