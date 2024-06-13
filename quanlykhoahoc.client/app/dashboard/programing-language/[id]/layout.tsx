import type { Metadata } from "next";
import { SubjectMapping } from "../../../web-api-client";

type Props = {
  params: { id: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const id = params.id;

  const data: SubjectMapping = await fetch(
    `${process.env.WEBSITE_URL}/api/subject/${id}`
  ).then((res) => res.json());

  return {
    title: data?.name,
  };
}
export default function DashboardProgramingLanguageLayout({ children }: { children: React.ReactNode }) {
  return children;
}
