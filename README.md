# 🐦 A simple implementation of the Boids algorithm in Unity

## 📜 Introduction

The **Boids** algorithm is a simulation model for the collective motion of entities (called _boids_, short for “bird-objects”), originally developed by **Craig Reynolds** in **1986**. It first appeared in the paper ["Flocks, Herds, and Schools: A Distributed Behavioral Model"](https://dl.acm.org/doi/abs/10.1145/37401.37406) published in 1987.

Boids simulate emergent flocking behavior using a small set of simple local rules applied to each agent, without any central control. This model has since been used in **computer graphics, robotics, AI simulations, and video games** to create realistic movement of crowds, animals, or autonomous drones.

## 🧭 Standard Boid Behaviors

Reynolds' original model is based on three core steering behaviors:

<br>

1. **🚫 Separation** - Avoid crowding neighbors

```math
   \mathbf{a}_{sep} = - \sum_{j \in N_i} \frac{\mathbf{p}_j - \mathbf{p}_i}{\|\mathbf{p}_j - \mathbf{p}_i\|^2}
```

<br>

2. **↔️ Alignment** - Match velocity with neighbors

```math
   \mathbf{a}_{ali} = \frac{1}{|N_i|} \sum_{j \in N_i} \mathbf{v}_j - \mathbf{v}_i
```

<br>

3. **🤝 Cohesion** - Move toward the average position of neighbors

```math
   \mathbf{a}_{coh} = \left( \frac{1}{|N_i|} \sum_{j \in N_i} \mathbf{p}_j \right) - \mathbf{p}_i
```

Where:

```math
\mathbf{p}_i = \text{position of boid i}
```

```math
\mathbf{v}_i = \text{velocity of boid i}
```

```math
N_i = \text{set of neighbors within a certain radius}
```

## 🚀 Additional Behaviors in This Project

4. **🛑 Collision Avoidance**

Avoids imminent collisions with obstacles using predictive sphere casting. If a potential collision is detected in the boid’s forward path, the boid chooses a safe alternative direction.

```math
\mathbf{a}_{col} = \text{ClampMag}(\mathbf{d}_{free} \cdot v_{max} - \mathbf{v}, f_{steer}) \cdot w_{avoidCol}
```

Where:

```math
\mathbf{d}_{free} = \text{first collision-free direction}
```

```math
v_{max} = \text{maximum speed}
```

```math
f_{steer} = \text{max steering force}
```

```math
w_{avoidCol} = \text{collision avoidance weight}
```

<br>

5. **📦 Bounds Avoidance**

Keeps boids inside a predefined 3D bounding area by steering them toward the center when they approach the boundary.

```math
\mathbf{a}_{bound} = \text{ClampMag}(\mathbf{c}_{center} \cdot v_{max} - \mathbf{v}, f_{steer}) \cdot w_{avoidBound}
```

Where:

```math
\mathbf{c}_{center} = \text{normalized vector toward the bounding box center}
```

```math
w_{avoidBound} = \text{bounds avoidance weight}
```

<br>

6. **🎯 Target Seeking**

Allows boids to seek a specific target position.

```math
\mathbf{a}_{target} = \text{ClampMag}(\hat{\mathbf{t}} \cdot v_{max} - \mathbf{v}, f_{steer}) \cdot w_{target}
```

Where:

```math
\hat{\mathbf{t}} = \text{normalized vector toward the target}
```

```math
w_{target} = \text{target seeking weight}
```

## ⚖️ Combined Steering

The final acceleration for each boid is computed as a weighted sum of all active behaviors:

```math
\mathbf{a}_{total} = w_{sep} \cdot \mathbf{a}_{sep} + w_{ali} \cdot \mathbf{a}_{ali} + w_{coh} \cdot \mathbf{a}_{coh} + w_{col} \cdot \mathbf{a}_{col} + w_{bound} \cdot \mathbf{a}_{bound} + w_{target} \cdot \mathbf{a}_{target}
```

Where each $w_x$ is a tunable parameter controlling the influence of the corresponding behavior.

## 🏁 Conclusion

This project follows **SOLID principles** 🧩 throughout its architecture, ensuring that each behavior, system, and component is modular, reusable, and easy to extend. The flocking logic is cleanly separated into independent behavior scripts that can be combined or swapped without breaking the core simulation - making the system adaptable to a wide range of applications.

Performance is further enhanced through the use of a **Compute Shader** ⚡, allowing the simulation to process thousands of boids in parallel directly on the GPU. The shader efficiently calculates flock statistics, such as neighbor count, heading, center of mass, and separation vectors...

This combination of clean, maintainable code and GPU-accelerated computation makes the project both scalable and future-proof.

<br>

_Happy coding! 🚀_
