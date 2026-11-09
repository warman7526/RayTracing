import pygame

TARGET = "./bin/Debug/net10.0/img2.ppm"
SCALE = 1

with open(TARGET,"r") as f:
    if f.readline().strip() != "P3":
        print("Incorrect image format")
        quit()
    WIDTH,HEIGHT = [int(s) for s in f.readline().strip().split(" ") if s]
    COL_DEPTH = int(f.readline().strip())
    PIXELS = []
    for line in f.readlines():
        PIXELS.append([int(s) for s in line.strip().split(" ") if s])
    if len(PIXELS) != WIDTH * HEIGHT:
        print("inconsistent dimensions")
        quit()

SCALE = max(round(600/min(WIDTH,HEIGHT),1),1)

pygame.init()

pygame.display.set_caption(f"[{WIDTH}x{HEIGHT}] ({TARGET})")

def redraw():
    screen = pygame.display.set_mode((WIDTH*SCALE,HEIGHT*SCALE))
    for index,col in enumerate(PIXELS):
        x = (index % WIDTH) * SCALE
        y = (index // WIDTH) * SCALE
        pygame.draw.rect(screen,col,pygame.Rect(x,y,int(SCALE+0.99),int(SCALE+0.99)))
    pygame.display.flip()

redraw()
while True:
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit()
            quit()
        elif event.type == pygame.KEYUP:
            if event.key == pygame.K_UP:
                SCALE += 0.1
            elif event.key == pygame.K_DOWN:
                            SCALE -= 0.1
            redraw()

