import { createSlice } from "@reduxjs/toolkit";

export const formSlice = createSlice({
  name: "form",
  initialState: {
    username: "",
    email: "",
    password: "",
    submitted: false
  },
  reducers: {
    setUsername: (state, action) => { state.username = action.payload; },
    setEmail: (state, action) => { state.email = action.payload; },
    setPassword: (state, action) => { state.password = action.payload; },
    setSubmitted: (state, action) => { state.submitted = action.payload; },
    clearForm: (state) => {
      state.username = "";
      state.email = "";
      state.password = "";
      state.submitted = false;
    }
  }
});

export const { setUsername, setEmail, setPassword, setSubmitted, clearForm } = formSlice.actions;
export default formSlice.reducer;
