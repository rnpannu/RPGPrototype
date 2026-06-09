import random
class person:
    # ppl walk around and talk. When a person speaks, they randomely select one previous statement they know, and then they say it. People 
    def __init__(self,init_name):
        self.name = init_name
        self.vocabulary = ["Hello, my name is " + self.name + ".", "Is it dinner yet? My stomach is rumbling.", 
                           "can you recommend anything to watch on Netflix?"]
    def talk(self):
        message = random.choice(self.vocabulary)
        return message
class parrot:
    # Parrots memorize everything they listen to, when they speak they randomely select one thing they have heard.
    # Parrots only know how to squak by defalut
    def __init__(self):
        self.vocabulary = ['SQUAK!']
        message = random.choice 
    def talk(self):
        message = random.choice(self.vocabulary)
        return message
    def listen(self, utterance):
        # updates the parrots vocabulary 
        self.vocabulary.append(utterance)
        
parrot1 = parrot()
for attempt in range(5):
    print(parrot1.talk())

print("------------")
parrot2 = parrot()
person1 = person("ben")
for exchange in range(7):
    parrot2.listen(person1.talk())
print("------------")
for attempt in range (0,5):
    print(parrot2.talk())
print(type(parrot1))
print(type(person1))