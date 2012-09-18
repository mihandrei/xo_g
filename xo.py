g1 = '''xxx
		 oo
		x'''

g2 = '''0
		 0o
		x 0'''

g3 = '''xx
		 oo
		x  '''				 

class state(object):		
	def __init__(self, gamestr):					
		v = []
		d = {'x': 1, 'o': -1 , '0':-1}		
		for s in gamestr:
			if s in d:
				v.append(d[s])
		if len(v) != 9:
			raise Exception("bad")						
		self.v = v
	
	def line_search(self, start, step):
		line = self.v[start + step*i] for i in range(3)
		s = set(line)
		if len(s) == 1:
			return s.pop()
		else:
			return 0

	def score(self):		
		def generate_
		for i in range(3):
			yield self.line_search(i*3,1)
			yield self.line_search(i,3)
		yield self.line_search(2,2)
		yield self.line_search(0,4)

		

	

	def expand(self):
		return []		
		
def search_min(state):	
	if state.isfinal():
		return state.score()
	else:		
		return min(search_max(ch) for ch in state.expand())

def search_max():
	pass	
