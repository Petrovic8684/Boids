# 🐦 A simple implementation of the Boids algorithm in Unity

## 📜 Introduction

The **Boids** algorithm is a simulation model for the collective motion of entities (called _boids_, short for “bird-objects”), originally developed by **Craig Reynolds** in **1986**. It first appeared in the paper ["Flocks, Herds, and Schools: A Distributed Behavioral Model"](https://dl.acm.org/doi/abs/10.1145/37401.37406) published in 1987.

Boids simulate emergent flocking behavior using a small set of simple local rules applied to each agent, without any central control. This model has since been used in **computer graphics, robotics, AI simulations, and video games** to create realistic movement of crowds, animals, or autonomous drones.

---

## 🧭 Standard Boid Behaviors

Reynolds' original model is based on three core steering behaviors:

<br>

1. **🚫 Separation** - Avoid crowding neighbors  
   \[
   \mathbf{a}_{sep} = -\sum_{j \in N_i} \frac{\mathbf{p}\_j - \mathbf{p}\_i}{\|\mathbf{p}\_j - \mathbf{p}\_i\|^2}
   \]

<br>

2. **↔️ Alignment** - Match velocity with neighbors  
   \[
   \mathbf{a}_{ali} = \frac{1}{|N_i|} \sum_{j \in N_i} \mathbf{v}\_j - \mathbf{v}\_i
   \]

<br>

3. **🤝 Cohesion** - Move toward the average position of neighbors  
   \[
   \mathbf{a}_{coh} = \left( \frac{1}{|N_i|} \sum_{j \in N_i} \mathbf{p}\_j \right) - \mathbf{p}\_i
   \]

Where:

- \( \mathbf{p}\_i \) = position of boid \( i \)
- \( \mathbf{v}\_i \) = velocity of boid \( i \)
- \( N_i \) = set of neighbors within a certain radius

---

## 🚀 Additional Behaviors in This Project

In addition to the original 3 rules, this Unity implementation introduces extra behaviors for more realistic and controlled movement:

<br>

4. **🛑 Collision Avoidance**

Avoids imminent collisions with obstacles using predictive sphere casting. If a potential collision is detected in the boid’s forward path, the boid chooses a safe alternative direction.

\[
\mathbf{a}_{col} = \text{ClampMag}\left( \mathbf{d}_{free} \cdot v*{max} - \mathbf{v}, f*{steer} \right) \cdot w\_{avoidCol}
\]

Where:

- \( \mathbf{d}\_{free} \) = first collision-free direction from a set of candidate directions
- \( v\_{max} \) = maximum speed
- \( f\_{steer} \) = max steering force
- \( w\_{avoidCol} \) = collision avoidance weight

<br>

5. **📦 Bounds Avoidance**

Keeps boids inside a predefined 3D bounding area by steering them toward the center when they approach the boundary.

\[
\mathbf{a}_{bound} = \text{ClampMag}\left( \mathbf{c}_{center} \cdot v*{max} - \mathbf{v}, f*{steer} \right) \cdot w\_{avoidBound}
\]

Where:

- \( \mathbf{c}\_{center} \) = normalized vector toward the bounding box center
- \( w\_{avoidBound} \) = bounds avoidance weight

<br>

5. **🎯 Target Seeking**

Allows boids to seek a specific target position.

\[
\mathbf{a}_{target} = \text{ClampMag}\left( \hat{\mathbf{t}} \cdot v_{max} - \mathbf{v}, f*{steer} \right) \cdot w*{target}
\]

Where:

- \( \hat{\mathbf{t}} \) = normalized vector toward the target
- \( w\_{target} \) = target seeking weight

---

## ⚖️ Combined Steering

The final acceleration for each boid is computed as a weighted sum of all active behaviors:

\[
\mathbf{a}_{total} = w_{sep} \cdot \mathbf{a}_{sep} + w_{ali} \cdot \mathbf{a}_{ali} + w_{coh} \cdot \mathbf{a}_{coh} + w_{col} \cdot \mathbf{a}_{col} + w_{bound} \cdot \mathbf{a}_{bound} + w_{target} \cdot \mathbf{a}\_{target}
\]

Where each \( w_x \) is a tunable parameter controlling the influence of the corresponding behavior.

---

## 🏁 Conclusion

This project follows **SOLID principles** 🧩 throughout its architecture, ensuring that each behavior, system, and component is modular, reusable, and easy to extend.  
The flocking logic is cleanly separated into independent behavior scripts that can be combined or swapped without breaking the core simulation - making the system adaptable to a wide range of applications.

Performance is further enhanced through the use of a **Compute Shader** ⚡, allowing the simulation to process thousands of boids in parallel directly on the GPU. The shader efficiently calculates flock statistics, such as neighbor count, heading, center of mass, and separation vectors...

This combination of clean, maintainable code and GPU-accelerated computation makes the project both scalable and future-proof.

<br>

_Happy coding! 🚀_
