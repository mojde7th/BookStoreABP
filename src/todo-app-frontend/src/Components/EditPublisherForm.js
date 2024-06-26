import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getPublisher, updatePublisher } from '../services/publisherService';

const EditPublisherForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publisher, setPublisher] = useState({ name: '' });

  useEffect(() => {
    getPublisher(id).then(response => setPublisher(response.data));
  }, [id]);

  const handleChange = e => {
    const { name, value } = e.target;
    setPublisher(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = e => {
    e.preventDefault();
    updatePublisher(id, publisher)
      .then(() => navigate('/publishers'))
      .catch(error => console.error('Error updating publisher:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={publisher.name} onChange={handleChange} required />
      </div>
      <button type="submit">Update</button>
    </form>
  );
};

export default EditPublisherForm;
