grade_book = {'Fred': 6 , 'Barney' : 12, 'Wilma': 3}
print(grade_book)
for key,value in grade_book.items():
    print(key, value) # .items() creates a tupple of each key/value pair in the dictionary

result = 0
for value in grade_book.values():
    result = result + value
    
print(result/len(grade_book))
