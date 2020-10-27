import math
import numpy as np
import scipy.integrate as integrate
from matplotlib import pyplot as plt


def ellipse(a, b, n, right=True):
    """
    :param a: semi-major axis (major axis length is 2a)
    :param b: semi-minor axis (minor axis length is 2b)
    :param n: number of sample points
    :param right: for specifying if drawing right or left set of ellipses, for arc length calc
    :return: x and y coordinates of ellipse, arc length
    """
    x_coord = []
    y_coord = []
    arcs = []
    thetas = np.linspace(0, 2 * math.pi, n+1)[:-1]
    for t in thetas:
        x = a * math.cos(t)
        y = b * math.sin(t)
        # Calculate arc length
        # TODO: move this calculation to flux function
        eqn = lambda tt: np.sqrt(a**2 * math.sin(tt)**2 + b**2 * math.cos(tt)**2)
        if right:
            t_init = math.pi
            d = integrate.quad(eqn, t_init, t)[0]
            if d < 0:
                half_len = integrate.quad(eqn, 0, math.pi)[0]
                d = half_len + (half_len + d)
            arcs.append(d)
        else:
            t_init = 0
            arcs.append(abs(integrate.quad(eqn, t_init, t)[0]))
        x_coord.append(x)
        y_coord.append(y)
    return x_coord, y_coord, arcs


def flux_lines(a0, b0, n, res):
    """
    :param a0: semi-major axis of smallest ellipse
    :param b0: semi-minor axis of smallest ellipse
    :param n: number of ellipses to draw
    :param res: number of sample points on each ellipse
    :return: x,y ellipse coords; u,v dir vectors; arc lengths to origin 0,0
    """
    x = []
    y = []
    u = []
    v = []
    arcs = []
    # Draw two sets of ellipses
    for e in range(2):
        for i in range(n):
            # this is where you change size/shape of successive ellipses
            a = a0 + i * a0
            b = b0 + i * b0
            if e == 0:
                x_i, y_i, arc = ellipse(a, b, res)
                x_i = [xx + (i + 1) * a0 for xx in x_i]
            else:
                x_i, y_i, arc = ellipse(a, b, res, right=False)
                x_i = [xx - (i + 1) * a0 for xx in x_i]
            x.extend(x_i)
            y.extend(y_i)
            arcs.extend(arc)
        if e == 0:
            start = 0
            end = len(x)
        else:
            start = int(len(x) / 2)
            end = len(x)
        # calculate elliptical vectors
        f_v = 0
        for i in range(start, end):
            if e == 1:
                if i > start:
                    u_i = x[i-1] - x[i]
                    v_i = y[i-1] - y[i]
            else:
                if i < len(x) - 1:
                    if (i + 1) % res == 0:
                        u_i = x[f_v] - x[i]
                        v_i = y[f_v] - y[i]
                        f_v += res
                    else:
                        u_i = x[i+1] - x[i]
                        v_i = y[i+1] - y[i]
            u.append(u_i / np.sqrt(u_i ** 2 + v_i ** 2))
            v.append(v_i / np.sqrt(u_i ** 2 + v_i ** 2))
    return x, y, u, v, arcs


def output_text(x, y, u, v, arcs, fname):
    out = 'coordinates\n'
    for i in range(len(x)):
        out += '%.4f' % x[i] + ' ' + '%.4f' % y[i] + '\n'
    out += '\nvectors\n'
    for i in range(len(u)):
        out += '%.4f' % u[i] + ' ' + '%.4f' % v[i] + '\n'
    out += '\narcs\n'
    for i in range(len(arcs)):
        out += '%.4f' % arcs[i] + '\n'
    out_file = open(fname, 'w')
    n = out_file.write(out)
    out_file.close()


# x, y = ellipse(3, 5, 20)
x, y, u, v, arcs = flux_lines(3, 5, 10, 30)
output_text(x, y, u, v, arcs, 'flux_field.txt')

print('len(x): ', len(x))
print('len(y): ', len(y))
print('len(u): ', len(u))
print('len(v): ', len(v))
print('len(arcs): ', len(arcs))

# plt.scatter(x, y, 5)
plt.quiver(x, y, u, v, scale=35)
plt.title('Flux field of avalanche beacon')
plt.axis('equal')
plt.xlabel('x-coord')
plt.ylabel('y-coord')
plt.show()
