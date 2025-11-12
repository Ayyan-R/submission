import React from 'react';
import { useSelector } from 'react-redux';

const Child2 = () => {
  const { username, email, password, submitted } = useSelector((state) => state.form);

  return (
    <div style={{ border: "1px solid gray", padding: "10px", margin: "10px" }}>
      <h2>Child 2 (Subscribed to Redux)</h2>
      {!submitted ? (
        <p>Waiting for submission</p>
      ) : (
        <pre>{JSON.stringify({ username, email, password }, null, 2)}</pre>
      )}
    </div>
  );
};

export default Child2;
