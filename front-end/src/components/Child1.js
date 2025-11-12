import React from 'react';
import { useSelector } from 'react-redux';

const Child1 = () => {
  const formData = useSelector((state) => state.form);

  return (
    <div style={{ border: "1px solid gray", padding: "10px", margin: "10px" }}>
      <h2>Child 1 (Read-only View)</h2>
      {formData.username || formData.email || formData.password ? (
        <pre>{JSON.stringify(formData, null, 2)}</pre>
      ) : (
        <p>waiting for submission...</p>
      )}
    </div>
  );
};

export default Child1;
