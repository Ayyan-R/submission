import React from 'react';

// Define main form component
function MyFormComponent({ children }) {
  return <form>{children}</form>;
}

// Define static subcomponents
function Input() {
  return <input type="text" placeholder="Type something" />;
}

function Button() {
  return <button type="submit">Submit</button>;
}

// Attach them as static properties
MyFormComponent.Input = Input;
MyFormComponent.Button = Button;

// Export a top-level component
export default function Component() {
  return (
    <MyFormComponent>
      <MyFormComponent.Input />
      <MyFormComponent.Button />
    </MyFormComponent>
  );
}
