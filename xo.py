class state(object):		
	def __init__(self, gamestr=None, tomove = 1):					
		d = {'x': 1, '*':1, 'o': -1 , '0':-1, ' ':0, '_' :0}
		self.tomove=tomove						
		
		if gamestr is None:
			self.v = [0]*9
		else:		
			v = [ d[s] for s in gamestr if s.lower() in d]				
			if len(v) != 9:
				raise Exception("bad"+str(v))						
			self.v = v
		
	def score(self):		
		line_indices = [[0,1,2],[3,4,5],[6,7,8],
				 		[0,3,6],[1,4,7],[2,5,8],
				 		[0,4,8],[2,4,6]]				
		for l in line_indices:
			line = [self.v[i] for i in l]			
			if line == [1,1,1]:
				return 1
			elif line == [-1,-1,-1]:
				return -1
		return 0					

	def expand(self):
		ret = []
		for i,c in enumerate(self.v):
			if c == 0:
				newstate = state()
				newstate.v = self.v[:]
				newstate.v[i] = self.tomove
				newstate.tomove = - self.tomove
				ret.append(newstate)
		return ret 

	def __repr__(self):
		d = ['_','X','0']
		v = [d[c] for c in self.v]
		return ''.join(v[0:3] + ['\n'] + v[3:6] + ['\n'] + v[6:9] + ['\n'])

def test1():	
	g1 = ('xxx' 
		  ' oo' 
		  'x  ')

	g2 = ('0  '
		  ' 00'
		  'x 0')

	g3 = ('xx '
		  ' oo'
		  ' x ')
	assert state(g1).score() == 1
	assert state(g2).score() == -1
	assert state(g3).score() == 0
	assert len(state(g1).expand()) == 3

def search_min(state):	
	if state.isfinal():
		return state.score()
	else:		
		return min(search_max(ch) for ch in state.expand())

def search_max():
	pass	

if __name__ == '__main__':
	test1()