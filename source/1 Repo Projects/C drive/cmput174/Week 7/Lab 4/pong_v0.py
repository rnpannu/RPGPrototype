import pygame

class Game:
    def __init__(self, surface):
        self.surface = surface
        self.bg_color = pygame.Color('black')
        self.FPS = 60
        self.game_Clock = pygame.time.Clock()
        self.close_clicked = False
        self.continue_game = True 
        
        # ---- Game specific objects ------
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
     
        
class Ball:
    def __init__(self, ball_color, ball_radius, ball_center, ball_velocity, surface):
        self.color = pygame.Color(ball_color)
        self.radius = ball_radius
        self.center = ball_center
        self.velocity = ball_velocity
        self.surface = surface
    def draw(self):
        pygame.draw.circle(self.surface, self.color, self.center, self.radius)
    def move(self):
        for i in range(0,2):
            self.center[i] = self.center[i] + self.velocity[i]
            if self.center[i] == radius or self.center == size[i] - radius:
                self.velocity[i] = -self.velocity[i]
def main():
    pygame.init()
    pygame.display.set_mode((500, 400))
    # set the title of the display window
    pygame.display.set_caption('A template for graphical games with two moving dots')   
    w_surface = pygame.display.get_surface() 
    # create a game object
    game = Game(w_surface)
    # start the main game loop by calling the play method on the game object
    game.play() 
    # quit pygame and clean up the pygame window
    pygame.quit()     
