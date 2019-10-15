import pickle

class MyUser(object) :
    def __init__(self, name):
        self.name = name


user = MyUser('vlado')
serialized = pickle.dumps(user)
filename = 'serialized.native'
print(serialized)

with open(filename,'wb') as file_object:
    file_object.write(serialized)


with open(filename,'rb') as file_object:
    raw_data = file_object.read()

deserialized = pickle.loads(raw_data)

print(file serialized.nat)