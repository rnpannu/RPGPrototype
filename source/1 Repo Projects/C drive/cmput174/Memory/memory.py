import pygame, random, time
def main():
   # initialize all pygame modules (some need initialization)
   pygame.init()
   # create a pygame display window
   pygame.display.set_mode((500, 400))
   # set the title of the display window
   pygame.display.set_caption('Memory')   
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

      # === objects that are part of every game that we will discuss ===
      self.surface = surface
      self.bg_color = pygame.Color('black')
      
      self.FPS = 60
      self.game_Clock = pygame.time.Clock()
      self.close_clicked = False
      self.continue_game = True
      
      # ======= game specific objects ========
      self.score = 0
      self.board_size = 4
      self.board = [] # will be represented by a list of lists
      self.image_list = []
      self.taking_inputs = True
      self.width = self.surface.get_width() //(self.board_size + 1)
      self.height = self.surface.get_height()//self.board_size       
      self.hidden = True
      self.create_board()
      self.revealed_tiles = []
      self.correct_guesses = []
   def load_images(self):
      # Loads the 8 unique images and appends them to a list. Doubles the list
      for i in range(1, 9):
         image = pygame.image.load('image' + str(i) + '.bmp')
         self.image_list.append(image)
      self.image_list = self.image_list + self.image_list      
      
   def create_board(self):
      # creates the 4x4 grid and puts tile objects in each coordinate.
      # Takes the images from the loaded list and assigns one to each tile.
      self.load_images()
      for row_index in range(0,self.board_size):
         row = []
         for col_index in range(0,self.board_size):
            tile_identity = self.image_list.pop(random.randint(0, len(self.image_list) - 1))
            tile_image = pygame.image.load('image0.bmp')
            x = col_index * self.width
            y = row_index * self.height
            tile = Tile(self.hidden, tile_image, tile_identity, x, y, self.width, self.height, self.surface)
            row.append(tile)
         self.board.append(row)
         
         
   def play(self):
      # Play the game until the player presses the close box.
      while not self.close_clicked:  
         # play frame
         self.handle_events()
         self.draw()            
         if self.continue_game:
            self.update()
            self.decide_continue()
         self.game_Clock.tick(self.FPS) # run at most with FPS Frames Per Second 

   def handle_events(self):
      # Handle each user event by changing the game state appropriately.
      # self.continue_game is a requirement for reveal clicks, as is self.taking
      # _inputs, which ensures you cannot click if two tiles are revealed
      events = pygame.event.get()
      for event in events:
         if event.type == pygame.QUIT:
            self.close_clicked = True
         if event.type == pygame.MOUSEBUTTONUP and self.continue_game and self.taking_inputs:
            self.handle_mouse_up(event.pos)
   def handle_mouse_up(self, pos):
      # For each tile, checks if it has been selected,
      # then reveals that tile and adds it to the condition list.
      # Conditions are: Cursor position has to be in the tile, less than 2 tiles
      # revealed tiles on the board, the same tile cannot be clicked, and corr
      # ect tiles cannot be clicked
      for row in self.board:
         for tile in row:
            if tile.select(pos) and len(self.revealed_tiles) < 2 and tile not in self.revealed_tiles and tile not in self.correct_guesses : 
               self.revealed_tiles.append(tile)
               tile.reveal()      
               
   def draw(self):
      # Draw all game objects
      self.surface.fill(self.bg_color) # clear the display surface first
      # draw each object in the board by calling the tile draw method
      for row in self.board:
         for tile in row:
            tile.draw()
      self.draw_score()
      pygame.display.update() # make the updated surface appear on the display
   def draw_score(self):
      # Draws the score to the upper-right corner of the screen
      score_string = str(self.score)
      fg_color = pygame.Color('white')
      font = pygame.font.SysFont('Comic Sans', 50)
      text_box = font.render(score_string, True, fg_color, self.bg_color)
      y = 0
      x = self.surface.get_width() - text_box.get_width()
      location = (x , y)      
      self.surface.blit(text_box, location)      
   def update(self):
      # Update the game objects for the next frame.
      # Hides wrong pairs, appends correct pairs to a list. Also stops 
      # reveal clicks if 2 tiles are revealed 
      self.score = pygame.time.get_ticks() // 1000
      if len(self.revealed_tiles) == 2:
         self.taking_inputs = False
         if self.revealed_tiles[0].match(self.revealed_tiles[1]):
            self.correct_guesses.append(self.revealed_tiles[0])
            self.correct_guesses.append(self.revealed_tiles[1])
         else:
            time.sleep(0.5)
            self.revealed_tiles[0].hide()
            self.revealed_tiles[1].hide()
         self.revealed_tiles = []
      else:
         self.taking_inputs = True
         
   def decide_continue(self):
      # Check and remember if the game should continue
      if len(self.correct_guesses) == (self.board_size)**2 :
         self.continue_game = False
      

class Tile:
   def __init__(self, hidden, image, identity, x,y,width,height,surface):
      # initializes all necessary instance attributes of a tile object
      self.rect = pygame.Rect(x,y,width,height)
      self.color = pygame.Color('black')
      self.border_width= 5
      self.surface = surface
      self.covered = image
      self.image = image
      self.hidden = hidden
      self.identity = identity
   def draw_content(self):  
      # draws the image to the tile location
      location = (self.rect.x, self.rect.y)
      self.surface.blit(self.image, location)
   def draw(self):
      # draws each tile and calls the function to draw its image
      pygame.draw.rect(self.surface, self.color, self.rect, self.border_width)
      self.draw_content()
   def select(self, position):
      # position is the (x,y) of the location of the click
      selected = False
      if self.rect.collidepoint(position): # Q1 is there a click?
         selected = True
      return selected
   def match(self, other):
      if self.identity == other.identity:
         return True
      else:
         return False
   # ======= Getter and setter methods =========
   def reveal(self):
      if self.hidden:
         self.image = self.identity
   def hide(self):
      self.image = self.covered
   def get_hidden(self):
      return self.hidden
   def get_identity(self):
      return self.identity
main()