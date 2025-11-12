import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { setUsername, setEmail, setPassword, setSubmitted, clearForm } from '../features/counter/formSlice';

const FormComponent = () => {
  const dispatch = useDispatch();
  const formData = useSelector((state) => state.form);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!formData.username || !formData.email || !formData.password) {
      alert("Please fill all fields");
      return;
    }
    dispatch(setSubmitted(true));
  };

  const handleReset = () => {
    dispatch(clearForm());
  };

  return (
    <form onSubmit={handleSubmit}>
      <h1>Login Form</h1>

      <div>
        <label>Username:</label>
        <input
          type="text"
          name="username"
          value={formData.username}
          onChange={(e) => dispatch(setUsername(e.target.value))}
        />
      </div>

      <div>
        <label>Email:</label>
        <input
          type="email"
          name="email"
          value={formData.email}
          onChange={(e) => dispatch(setEmail(e.target.value))}
        />
      </div>

      <div>
        <label>Password:</label>
        <input
          type="password"
          name="password"
          value={formData.password}
          onChange={(e) => dispatch(setPassword(e.target.value))}
        />
      </div>

      <button type="submit">Submit</button>
      <button type="button" onClick={handleReset}>Reset</button>
    </form>
  );
};

export default FormComponent;
