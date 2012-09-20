class state(object):		
	def __init__(self, gamestr=None, tomove = 1):					
		d = {'x': 1, '*':1, 'o': -1 , '0':-1, ' ':0, '_' :0}
		self.tomove = tomove						
		
		if gamestr is None:
			self.v = [0]*9
		else:		
			v = [ d[s] for s in gamestr if s.lower() in d]				
			if len(v) != 9:
				raise Exception("bad"+str(v))						
			self.v = v
		
	def goal(self):		
		line_indices = [[0,1,2],[3,4,5],[6,7,8],
				 		[0,3,6],[1,4,7],[2,5,8],
				 		[0,4,8],[2,4,6]]				
		for l in line_indices:
			line = [self.v[i] for i in l]			
			if line == [1,1,1]:
				return 1
			elif line == [-1,-1,-1]:
				return -1
		
		if 0 in self.v:
			return None 
		else:
			return 0 #no more moves

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

def minmax(state, side):
	def snd (t):
		return t[0]

	goal = state.goal()
	if goal is not None:
		return (goal, None)
	else:		
		children = state.expand()		
		scores = [minmax(c, -side)[0] for c in children]		
		children_score = zip(scores, children)
		if side == 1:
			return max(children_score, key=snd)
		else:
			return min(children_score, key=snd)

def ai_play():
	def readmove():
		m = input()
		return m

	st = state()

	while st.goal() is None:
		m = readmove()
		st.v[m] = 1
		st.tomove = -st.tomove
		print st
		g, st = minmax(st,-1)
		print st

	print 'winner' , st.goal()
	input()

g1 = ('xxx' 
	  ' oo' 
	  'x  ')

g2 = ('0  '
	  ' 00'
	  'x 0')

g3 = ('xo '
	  ' oo'
	  ' xx')

g4 = ('x 0'
	  'xx '
	  ' 0 ')

def test1():		
	assert state(g1).goal() == 1
	assert state(g2).goal() == -1
	assert state(g3).goal() == None
	assert len(state(g1).expand()) == 3

def test2():
	st = state(g3,1)		
	g, nst = minmax(st,1)
	assert g == 1
	assert nst.v[6] == 1

	st = state(g3, -1)	
	g, nst = minmax(st,-1)
	assert g == -1	
	assert nst.v[3] == -1

if __name__ == '__main__':
	test1()
	test2()
	ai_play()