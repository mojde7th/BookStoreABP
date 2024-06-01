import axios from "axios";

const { useState, useEffect } = require("react")



const AuthorList=()=>{
    const [authors,setAuthors]=useState([]);
    const [newAuthor,setNewAuthor]=useState({
        name:'', bio:''
    });

    useEffect(()=>{
        fetchAuthors();
    },[]);

    const fetchAuthors = async ()=>{

        try{
            const response=await axios.get('https://localhost:44388/api/authors');
       setAuthors(response.data);
        }
        catch(error){
            console.error('error fetching authors:',error);
    }};
        const handleInputChange=(e)=>{
            const {name, value}=e.target;
            setNewAuthor({...newAuthor,[name]:value});
        };

        const handleSubmit=async (e)=>{
            e.preventDefault();
            try{
                await axios.post('https://localhost:44388/api/authors',newAuthor);
                setNewAuthor({name:'',bio:''});
                fetchAuthors();
            }
            catch (error){
                console.error('Error creating Author:',error);
            }};
            return(
                <div>
<h1>Author List</h1>

<form onSubmit={handleSubmit}>
<input type="text" name="name" value={newAuthor.name}
onChange={handleInputChange} placeholder="Name"
/>
<input type="text" name="bio" value={newAuthor.bio}
onChange={handleInputChange} placeholder="Bio"
/>
<button type="submit"> Add Author</button>
</form>

<ul>
    {authors.map((author)=>(<li key={author.id}>

        {author.name}:{author.bio}
    </li>))}
</ul>
                </div>
            );
        };
    export default AuthorList
